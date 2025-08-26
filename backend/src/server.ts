import dotenv from 'dotenv';
import express from 'express';
import cors from 'cors';
import morgan from 'morgan';

import authRoutes from './routes/auth.routes';
import { auth, AuthRequest } from './middleware/auth';

dotenv.config();

const app = express();
app.use(cors({ origin: true }));
app.use(express.json());
app.use(morgan('dev'));

app.get('/health', (_req, res) => res.json({ ok: true }));
app.use('/auth', authRoutes);

app.get('/protected', auth(true), (req: AuthRequest, res) => {
    if (!req.user) return res.status(401).json({ error: 'Unauthorized' });
    res.json({ message: `Hello ${req.user.username}!` });
});

const port = process.env.PORT || 3000;
app.listen(port, () => {
    console.log(`API running on http://localhost:${port}`);
});
