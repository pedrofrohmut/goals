DROP TABLE IF EXISTS users;

DROP TABLE IF EXISTS goals;

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE users (
    id UUID DEFAULT uuid_generate_v4(),
    name TEXT NOT NULL,
    email TEXT NOT NULL,
    password_hash TEXT NOT NULL,
    phone TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT now(),
    PRIMARY KEY(id)
);

CREATE TABLE goals (
    id UUID DEFAULT uuid_generate_v4(),
    text TEXT NOT NULL,
    user_id UUID NOT NULL,
    created_at TIMESTAMP DEFAULT now(),
    PRIMARY KEY(id),
    CONSTRAINT fk_goals_user FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);
