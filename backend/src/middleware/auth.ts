import { Request, Response, NextFunction } from 'express';
import jwt, { JwtPayload } from 'jsonwebtoken';

export interface UserPayload {
    id: number;
    email: string;
    username: string;
}

export interface AuthRequest extends Request {
    user?: UserPayload | null;
}

export function auth(required = true) {
    return (req: AuthRequest, res: Response, next: NextFunction) => {
        const header = req.headers['authorization'] || '';
        const token = header.startsWith('Bearer ') ? header.slice(7) : null;

        if (!token) {
            if (required) return res.status(401).json({ error: 'No token' });
            req.user = null;
            return next();
        }

        try {
            const secret = process.env.JWT_SECRET as string;
            const payload = jwt.verify(token, secret) as JwtPayload &
                UserPayload;
            req.user = {
                id: payload.id,
                email: payload.email,
                username: payload.username,
            };
            next();
        } catch {
            return res.status(401).json({ error: 'Invalid token' });
        }
    };
}
