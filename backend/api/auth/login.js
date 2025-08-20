import { supabase, corsHeaders } from '../_lib/supabase.js';

export default async function handler(req, res) {
  if (req.method === 'OPTIONS') {
    return res.status(200).end();
  }

  if (req.method !== 'POST') {
    return res.status(405).json({ 
      error: 'Method not allowed' 
    });
  }

  try {
    const { email, password } = req.body;

    // Supabase Auth로 로그인
    const { data, error } = await supabase.auth.signInWithPassword({
      email,
      password
    });

    if (error) {
      return res.status(401).json({
        error: '이메일 또는 비밀번호가 일치하지 않습니다.'
      });
    }

    // 프로필 정보 가져오기
    const { data: profile } = await supabase
      .from('profiles')
      .select('username')
      .eq('id', data.user.id)
      .single();

    return res.status(200).json({
      message: '로그인 성공',
      session: data.session,
      user: {
        id: data.user.id,
        email: data.user.email,
        username: profile?.username
      }
    });

  } catch (error) {
    console.error('Login error:', error);
    return res.status(500).json({
      error: '서버 오류가 발생했습니다.'
    });
  }
}