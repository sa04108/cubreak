import { Request, Response } from 'express';
import bcrypt from 'bcrypt';
import jwt from 'jsonwebtoken';
import mysql from 'mysql2/promise';
import nodemailer from 'nodemailer';
import { pool } from '../config/db';
import { UserPayload, AuthRequest } from '../middleware/auth';
import { User } from '../entity/User';
import { AppDataSource } from '../data-source';

const createToken = (u: UserPayload): string =>
    jwt.sign(
        { id: u.id, email: u.email, username: u.username },
        process.env.JWT_SECRET as string,
        { expiresIn: '1h' },
    );

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

export async function requestMe(req: AuthRequest, res: Response) {
    return res.json({ user: req.user });
}

export async function requestPasswordReset(req: Request, res: Response) {
    try {
        const { email } = req.body;
        if (!email) {
            return res.status(400).json({ message: 'Email is required' });
        }

        // 1. 유저 확인
        const repo = AppDataSource.getRepository(User);
        const profile = await repo.findOne({
            where: { email },
        });
        if (!profile) {
            // 보안상 이메일 존재 여부 노출 X
            return res.json({
                message:
                    '등록된 이메일 주소로 임시 비밀번호를 발송했습니다. (존재하지 않더라도 동일 메시지)',
            });
        }

        // 2. 임시 비밀번호 생성
        const tempPassword = Math.random().toString(36).slice(-10);

        // 3. 해싱 후 DB 저장
        const hashed = await bcrypt.hash(tempPassword, 10);
        profile.password_hash = hashed;
        await AppDataSource.manager.save(profile);

        // 4. 메일 발송
        const transporter = nodemailer.createTransport({
            service: 'gmail',
            auth: {
                user: process.env.SMTP_USER,
                pass: process.env.SMTP_PASS,
            },
        });

        await transporter.sendMail({
            from: `"관리자" <${process.env.SMTP_USER}>`,
            to: email,
            subject: '임시 비밀번호 안내',
            text: `임시 비밀번호: ${tempPassword}\n\n로그인 후 반드시 비밀번호를 변경해주세요.`,
        });

        return res.json({
            message: '등록된 이메일 주소로 임시 비밀번호를 발송했습니다.',
        });
    } catch (error) {
        console.error('reset-password error:', error);
        return res.status(500).json({ message: 'Internal Server Error' });
    }
}
