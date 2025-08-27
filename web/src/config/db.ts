import dotenv from 'dotenv';
import mysql from 'mysql2/promise';

dotenv.config();

export const pool = mysql.createPool({
    host: process.env.DB_HOST as string,
    port: Number(process.env.DB_PORT) || 3306,
    database: process.env.DB_DATABASE as string,
    user: process.env.DB_USER as string,
    password: process.env.DB_PASSWORD as string,
    connectionLimit: 10,
});
