// dotenv로 로컬 환경 변수 가져오기
// https://www.npmjs.com/package/dotenv
// .env.local로 vercel 환경 변수 가져오기: vercel env pull .env.local
import dotenv from 'dotenv'
dotenv.config({ path: '.env.local' })

import { createClient } from '@supabase/supabase-js';

export const supabaseClient = createClient(
  process.env.SUPABASE_URL,
  process.env.SUPABASE_ANON_KEY
);

// CORS 헤더 (Unity WebGL용)
export const corsHeaders = {
  'Access-Control-Allow-Origin': '*',
  'Access-Control-Allow-Methods': 'POST, GET, OPTIONS',
  'Access-Control-Allow-Headers': 'Content-Type, Authorization',
};