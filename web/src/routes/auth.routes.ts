import { Router } from 'express';
import {
    requestLogin,
    requestMe,
    requestPasswordReset,
    requestRegister,
} from '../controllers/auth.controller';
import { auth } from '../middleware/auth';

const router = Router();

// 회원가입
router.post('/register', requestRegister);

// 로그인
router.post('/login', requestLogin);

// 내 정보
router.get('/me', auth(true), requestMe);

// 비밀번호 재발급 (임시 비밀번호 메일 발송)
router.post('/reset-password', requestPasswordReset);

export default router;
