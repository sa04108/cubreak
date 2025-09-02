import dotenv from 'dotenv';
import 'reflect-metadata';
import { DataSource } from 'typeorm';
import { User } from './entity/User';
import { UserTemporaryToken } from './entity/UserTemporaryToken';

dotenv.config();

export const AppDataSource = new DataSource({
    type: 'mysql', // or postgres, sqlite 등
    host: process.env.DB_HOST,
    port: Number(process.env.DB_PORT),
    username: process.env.DB_USER,
    password: process.env.DB_PASSWORD,
    database: process.env.DB_DATABASE,
    synchronize: false, // 개발 단계에서는 true, 운영에서는 migration 사용
    logging: false,
    entities: [User, UserTemporaryToken],
    migrations: [],
    subscribers: [],
});
