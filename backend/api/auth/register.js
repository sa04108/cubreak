import { supabase, corsHeaders } from '../_lib/supabase.js';

export default async function handler(req, res) {
  // CORS preflight 처리
  if (req.method === 'OPTIONS') {
    return res.status(200).end();
  }

  if (req.method !== 'POST') {
    return res.status(405).json({ 
      error: 'Method not allowed' 
    });
  }

  try {
    const { email, password, username } = req.body;

    // 입력 검증
    if (!email || !password || !username) {
      return res.status(400).json({
        error: '모든 필드를 입력해주세요.'
      });
    }

    // username 중복 체크
    const { data: existingUser } = await supabase
      .from('profiles')
      .select('username')
      .eq('username', username)
      .single();

    if (existingUser) {
      return res.status(400).json({
        error: '이미 사용중인 사용자명입니다.'
      });
    }

    // Supabase Auth로 회원가입
    const { data, error } = await supabase.auth.signUp({
      email,
      password,
      options: {
        data: {
          username: username
        }
      }
    });

    if (error) {
      return res.status(400).json({
        error: error.message
      });
    }

    // 세션 토큰 반환
    return res.status(200).json({
      message: '회원가입 성공',
      session: data.session,
      user: {
        id: data.user.id,
        email: data.user.email,
        username: username
      }
    });

  } catch (error) {
    console.error('Register error:', error);
    return res.status(500).json({
      error: '서버 오류가 발생했습니다.'
    });
  }
}

// Vercel Edge Config (선택사항 - 더 빠른 응답)
export const config = {
  runtime: 'edge',
};