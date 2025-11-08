export interface User {
    id: number;
    username: string;
    email: string;
    bio?: string;
    profilePicture?: string;
    createdAt: string;
}

export interface Post {
    id: number;
    content: string;
    imageUrl?: string;
    createdAt: string;
    user: User;
    likeCount: number;
    commentCount: number;
}

export interface Comment {
    id: number;
    content: string;
    createdAt: string;
    user: User;
    postId: number;
}

export interface AuthResponse {
    token: string;
    user: User;
}

export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
    username: string;
    email: string;
    password: string;
}