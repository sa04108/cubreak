import { supabase, corsHeaders } from '../_lib/supabase.js';

export default async function handler(req, res) {
  if (req.method === 'OPTIONS') {
    return res.status(200).end();
  }

  if (req.method !== 'GET') {
    return res.status(405).json({ 
      error: 'Method not allowed' 
    });
  }

  try {
    const token = req.headers.authorization?.replace('Bearer ', '');
    
    if (!token) {
      return res.status(401).json({
        error: '토큰이 없습니다.'
      });
    }

    // Supabase로 토큰 검증
    const { data: { user }, error } = await supabase.auth.getUser(token);

    if (error || !user) {
      return res.status(401).json({
        error: '유효하지 않은 토큰입니다.'
      });
    }

    // 프로필 정보 가져오기
    const { data: profile } = await supabase
      .from('profiles')
      .select('username')
      .eq('id', user.id)
      .single();

    return res.status(200).json({
      user: {
        id: user.id,
        email: user.email,
        username: profile?.username
      }
    });

  } catch (error) {
    console.error('Verify error:', error);
    return res.status(500).json({
      error: '서버 오류가 발생했습니다.'
    });
  }
}