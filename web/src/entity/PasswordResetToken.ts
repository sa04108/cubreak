import {
    Entity,
    PrimaryGeneratedColumn,
    Column,
    ManyToOne,
    JoinColumn,
    CreateDateColumn,
    BaseEntity,
} from 'typeorm';
import { User } from './User';

@Entity('password_reset_tokens')
export class PasswordResetToken extends BaseEntity {
    @PrimaryGeneratedColumn()
    id!: number;

    @ManyToOne(() => User)
    @JoinColumn({ name: 'user_id' })
    user!: User;

    @Column()
    user_id!: number;

    @Column({ unique: true })
    token!: string;

    @Column()
    expires_at!: Date;

    @CreateDateColumn()
    created_at!: Date;
}
