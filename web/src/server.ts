import express from 'express';
import cors from 'cors';
import morgan from 'morgan';
import path from 'path';

import authRoutes from './routes/auth.routes';
import { auth, AuthRequest } from './middleware/auth';

export function startServer() {
    const app = express();
    app.use(cors({ origin: true, methods: ['GET', 'POST'] }));
    app.use(express.json());
    app.use(morgan('dev'));

    // Unity WebGL 빌드 파일 경로
    const unityPath = path.join(__dirname, '../unity');
    app.use(express.static(unityPath));

    // API routes
    app.get('/health', (_req, res) => res.json({ ok: true }));
    app.use('/auth', authRoutes);

    app.get('/protected', auth(true), (req: AuthRequest, res) => {
        if (!req.user) return res.status(401).json({ error: 'Unauthorized' });
        res.json({ message: `Hello ${req.user.username}!` });
    });

    app.get('/reset-password', (_req, res) => {
        res.sendFile(path.join(__dirname, '../reset-password.html'));
    });

    // Unity WebGL index.html 서빙 (SPA 대응)
    app.get('/{*any}', (_req, res) => {
        res.sendFile(path.join(unityPath, 'index.html'));
    });

    const port: number = Number(process.env.PORT) || 3000;
    const host: string = process.env.HOST || '0.0.0.0';

    app.listen(port, host, () => {
        console.log(`API running on http://${host}:${port}`);
    });
}
