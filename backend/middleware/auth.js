// auth.js
const jwt = require('jsonwebtoken');

function auth(required = true) {
  return (req, res, next) => {
    const header = req.headers['authorization'] || '';
    const token = header.startsWith('Bearer ') ? header.slice(7) : null;

    if (!token) {
      if (required) return res.status(401).json({ error: 'No token' });
      req.user = null;
      return next();
    }

    try {
      const payload = jwt.verify(token, process.env.JWT_SECRET);
      req.user = { id: payload.id, email: payload.email, username: payload.username };
      next();
    } catch (e) {
      return res.status(401).json({ error: 'Invalid token' });
    }
  };
}

module.exports = { auth };
