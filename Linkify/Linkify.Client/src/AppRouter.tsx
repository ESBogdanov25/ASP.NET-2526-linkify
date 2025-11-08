import React from 'react';
import { BrowserRouter, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import FeedPage from './pages/FeedPage';

const AppRouter: React.FC = () => {
    return (
        <BrowserRouter>
            <Route path="/login" Component={LoginPage} />
            <Route path="/register" Component={RegisterPage} />
            <Route path="/" Component={FeedPage} />
        </BrowserRouter>
    );
};

export default AppRouter;