import { Request, Response } from 'express';
import bcrypt from 'bcrypt';
import jwt from 'jsonwebtoken';
import mysql from 'mysql2/promise';
import nodemailer from 'nodemailer';
import { pool } from '../config/db';
import { UserPayload, AuthRequest } from '../middleware/auth';
import { User } from '../entity/User';
import { AppDataSource } from '../data-source';
import { UserTemporaryToken } from '../entity/UserTemporaryToken';

const createToken = (u: UserPayload): string =>
    jwt.sign(
        { id: u.id, email: u.email, username: u.username },
        process.env.JWT_SECRET as string,
        { expiresIn: '1h' },
    );

// req body: { email, username, password }
export async function requestRegister(req: Request, res: Response) {
    try {
        const {
            email = '',
            username = '',
            password = '',
        } = req.body as {
            email?: string;
            username?: string;
            password?: string;
        };

        if (!email || !username || !password)
            return res.status(400).json({ error: 'Missing fields' });
        if (password.length < 6)
            return res.status(400).json({ error: 'Password too short' });

        const hash = await bcrypt.hash(password, 12);

        const [result] = await pool.execute<mysql.ResultSetHeader>(
            `INSERT INTO users (email, username, password_hash) VALUES (?, ?, ?)`,
            [email.trim().toLowerCase(), username.trim().toLowerCase(), hash],
        );

        const insertId = (result as mysql.ResultSetHeader).insertId;

        const [rows] = await pool.execute<any[]>(
            `SELECT id, email, username, created_at FROM users WHERE id = ?`,
            [insertId],
        );

        const user = rows[0];
        const token = createToken(user);
        return res.status(201).json({ user, token });
    } catch (e: any) {
        if (e.code === 'ER_DUP_ENTRY') {
            return res
                .status(409)
                .json({ error: 'Email or username already exists' });
        } else if (e.code === 'ER_ACCESS_DENIED_ERROR') {
            return res.status(401).json({
                error: 'MySQL Access denied. Check db username or password',
            });
        } else if (e.code === 'ECONNREFUSED') {
            return res.status(503).json({
                error: 'MySQL Connection Failed. DB service is not available now',
            });
        }
        console.error(e);
        return res.status(500).json({ error: 'Server error' });
    }
}

// req body: { emailOrUsername, password }
export async function requestLogin(req: Request, res: Response) {
    try {
        const { emailOrUsername = '', password = '' } = req.body as {
            emailOrUsername?: string;
            password?: string;
        };

        if (!emailOrUsername || !password)
            return res.status(400).json({ error: 'Missing fields' });

        const [rows] = await pool.execute<any[]>(
            `SELECT id, email, username, password_hash FROM users WHERE email = ? OR username = ? LIMIT 1`,
            [
                emailOrUsername.trim().toLowerCase(),
                emailOrUsername.trim().toLowerCase(),
            ],
        );

        const row = rows[0];
        if (!row) return res.status(401).json({ error: 'User not found' });

        const ok = await bcrypt.compare(password, row.password_hash);
        if (!ok) return res.status(401).json({ error: 'Wrong password' });

        const user: UserPayload = {
            id: row.id,
            email: row.email,
            username: row.username,
        };
        const token = createToken(user);
        return res.json({ user, token });
    } catch (e) {
        console.error(e);
        return res.status(500).json({ error: 'Server error' });
    }
}

// req header: {Authorization: Bearer ~}
export async function requestMe(req: AuthRequest, res: Response) {
    return res.json({ user: req.user });
}

// req body: email
export async function requestResetPassword(req: Request, res: Response) {
    try {
        const { email } = req.body;
        const userRepo = AppDataSource.getRepository(User);
        const tokenRepo = AppDataSource.getRepository(UserTemporaryToken);

        const user = await userRepo.findOne({ where: { email } });
        if (!user) {
            return res.status(404).json({ message: 'Unregistered email' });
        }

        // JWT 토큰 생성
        const token = jwt.sign(
            { user_id: user.id },
            process.env.JWT_SECRET as string,
            {
                expiresIn: '30m',
            },
        );

        // DB 저장
        const expiresAt = new Date(Date.now() + 30 * 60 * 1000);
        const resetToken = tokenRepo.create({
            user_id: user.id,
            token,
            expires_at: expiresAt,
        });
        await tokenRepo.save(resetToken);

        // Email 전송
        await sendResetPasswordMail(user.email, token);

        return res.json({
            message: 'The email for password reset has been sent successfully.',
        });
    } catch (err) {
        console.error(err);
        res.status(500).json({ error: 'Server error' });
    }
}

// req body: { email, token }
async function sendResetPasswordMail(to: string, token: string) {
    const resetLink = `http://localhost:3000/reset-password?token=${token}`;

    const transporter = nodemailer.createTransport({
        service: 'gmail',
        auth: {
            user: process.env.SMTP_USER,
            pass: process.env.SMTP_PASS,
        },
    });

    await transporter.sendMail({
        from: `"Cubreak" <${process.env.SMTP_USER}>`,
        to,
        subject: 'Password Reset Instructions',
        html: `
            <p>To reset your password, please click the link below:</p>
            <a href="${resetLink}">${resetLink}</a>
            <p>This link is valid for 30 minutes only.</p>
            <span style="color: #ff0000">Do not share this link with anyone.</span>
        `,
    });
}

// req body: { token, password }
export async function requestChangePassword(req: Request, res: Response) {
    try {
        const { token, password } = req.body;
        const userRepo = AppDataSource.getRepository(User);
        const tokenRepo = AppDataSource.getRepository(UserTemporaryToken);

        // JWT 검증
        const payload = jwt.verify(token, process.env.JWT_SECRET as string) as {
            user_id: number;
        };

        // DB에서 토큰 확인
        const tokenRecord = await tokenRepo.findOne({ where: { token } });
        if (!tokenRecord || tokenRecord.expires_at < new Date()) {
            return res
                .status(400)
                .json({ message: 'Token is either invalid or expired.' });
        }

        const hashedPassword = await bcrypt.hash(password, 10);
        await userRepo.update(
            { id: payload.user_id },
            { password_hash: hashedPassword },
        );
        await tokenRepo.delete({ token });

        return res.json({ message: 'Your password successfully changed.' });
    } catch (error) {
        console.error('reset-password error:', error);
        return res.status(400).json({ message: 'Bad request' });
    }
}
