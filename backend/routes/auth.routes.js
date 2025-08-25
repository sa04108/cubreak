// routes.auth.js
const express = require('express');
const bcrypt = require('bcrypt');
const jwt = require('jsonwebtoken');
const pool = require('../config/db');
const { auth } = require('../middleware/auth');

const router = express.Router();

const createToken = (u) =>
  jwt.sign({ id: u.id, email: u.email, username: u.username }, process.env.JWT_SECRET, { expiresIn: '7d' });

// 회원가입
router.post('/register', async (req, res) => {
  try {
    const { email = '', username = '', password = '' } = req.body || {};
    if (!email || !username || !password) return res.status(400).json({ error: 'Missing fields' });
    if (password.length < 6) return res.status(400).json({ error: 'Password too short' });

    const hash = await bcrypt.hash(password, 12);

    const [result] = await pool.execute(
      `INSERT INTO users (email, username, password_hash) VALUES (?, ?, ?)`,
      [email.trim().toLowerCase(), username.trim().toLowerCase(), hash]
    );

    const [rows] = await pool.execute(
      `SELECT id, email, username, created_at FROM users WHERE id = ?`,
      [result.insertId]
    );

    const user = rows[0];
    const token = createToken(user);
    return res.status(201).json({ user, token });
  } catch (e) {
    if (e.code === 'ER_DUP_ENTRY') { // 엄격 비교
      return res.status(409).json({ error: 'Email or username already exists' });
    }
    else if (e.code === 'ER_ACCESS_DENIED_ERROR') {
      return res.status(401).json({ error: 'MySQL Access denied. Check db username or password' });
    }
    else if (e.code === 'ECONNREFUSED') {
      return res.status(503).json({ error: 'MySQL Connection Failed. DB service is not available now' })
    }
    console.error(e);
    return res.status(500).json({ error: 'Server error' });
  }
});

// 로그인
router.post('/login', async (req, res) => {
  try {
    const { emailOrUsername = '', password = '' } = req.body || {};
    if (!emailOrUsername || !password) return res.status(400).json({ error: 'Missing fields' });

    const [rows] = await pool.execute(
      `SELECT id, email, username, password_hash FROM users WHERE email = ? OR username = ? LIMIT 1`,
      [emailOrUsername.trim().toLowerCase(), emailOrUsername.trim().toLowerCase()]
    );

    const row = rows[0];
    if (!row) return res.status(401).json({ error: 'Invalid credentials' });

    const ok = await bcrypt.compare(password, row.password_hash);
    if (!ok) return res.status(401).json({ error: 'Invalid credentials' });

    const user = { id: row.id, email: row.email, username: row.username };
    const token = createToken(user);
    return res.json({ user, token });
  } catch (e) {
    console.error(e);
    return res.status(500).json({ error: 'Server error' });
  }
});

// 내 정보
router.get('/me', auth(true), (req, res) => {
  return res.json({ user: req.user });
});

module.exports = router;
