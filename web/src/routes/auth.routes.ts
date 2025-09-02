import { Router } from 'express';
import {
    requestLogin,
    requestMe,
    requestResetPassword,
    requestRegister,
    requestChangePassword,
} from '../controllers/auth.controller';
import { auth } from '../middleware/auth';

const router = Router();

// 회원가입
router.post('/register', requestRegister);

// 로그인
router.post('/login', requestLogin);

// 내 정보
router.get('/me', auth(true), requestMe);

// 비밀번호 재발급 (비밀번호 초기화 메일 발송)
router.post('/reset-password', requestResetPassword);

// 비밀번호 변경
router.post('/change-password', requestChangePassword);

export default router;
