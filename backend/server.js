// server.js
require('dotenv').config();
const express = require('express');
const cors = require('cors');
const morgan = require('morgan');

const authRoutes = require('./routes/auth.routes');
const { auth } = require('./middleware/auth');

const app = express();
app.use(cors({ origin: true }));
app.use(express.json());
app.use(morgan('dev'));

app.get('/health', (_req, res) => res.json({ ok: true }));
app.use('/auth', authRoutes);

app.get('/protected', auth(true), (req, res) => {
  res.json({ message: `Hello ${req.user.username}!` });
});

const port = process.env.PORT || 3000;
app.listen(port, () => {
  console.log(`API running on http://localhost:${port}`);
});
