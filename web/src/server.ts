import dotenv from 'dotenv';
import express from 'express';
import cors from 'cors';
import morgan from 'morgan';
import path from 'path';

import authRoutes from './routes/auth.routes';
import { auth, AuthRequest } from './middleware/auth';

dotenv.config();

const app = express();
app.use(cors({ origin: true }));
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

// Unity WebGL index.html 서빙 (SPA 대응)
app.get('/{*any}', (_req, res) => {
    res.sendFile(path.join(unityPath, 'index.html'));
});

const port = process.env.PORT || 3000;
app.listen(port, () => {
    console.log(`API running on http://localhost:${port}`);
    console.log(`Frontend running on http://localhost:${port}`);
});
