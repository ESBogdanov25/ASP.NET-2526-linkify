import React from 'react';

const LoginPage: React.FC = () => {
    return (
        <div style={{
            minHeight: '100vh',
            backgroundColor: '#f9fafb',
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'center',
            padding: '3rem 1rem'
        }}>
            <div style={{ maxWidth: '28rem', margin: '0 auto', width: '100%' }}>
                <h2 style={{ textAlign: 'center', fontSize: '1.875rem', fontWeight: 'bold', color: '#111827' }}>
                    Sign in to Linkify
                </h2>

                <div style={{
                    backgroundColor: 'white',
                    padding: '2rem 1rem',
                    borderRadius: '0.5rem',
                    boxShadow: '0 1px 3px 0 rgba(0, 0, 0, 0.1)',
                    marginTop: '2rem'
                }}>
                    <form>
                        <div style={{ marginBottom: '1.5rem' }}>
                            <label style={{ display: 'block', fontSize: '0.875rem', fontWeight: '500', color: '#374151' }}>
                                Email address
                            </label>
                            <input
                                type="email"
                                required
                                style={{
                                    width: '100%',
                                    borderRadius: '0.375rem',
                                    border: '1px solid #d1d5db',
                                    padding: '0.5rem 0.75rem',
                                    marginTop: '0.25rem'
                                }}
                            />
                        </div>

                        <div style={{ marginBottom: '1.5rem' }}>
                            <label style={{ display: 'block', fontSize: '0.875rem', fontWeight: '500', color: '#374151' }}>
                                Password
                            </label>
                            <input
                                type="password"
                                required
                                style={{
                                    width: '100%',
                                    borderRadius: '0.375rem',
                                    border: '1px solid #d1d5db',
                                    padding: '0.5rem 0.75rem',
                                    marginTop: '0.25rem'
                                }}
                            />
                        </div>

                        <button
                            type="submit"
                            style={{
                                width: '100%',
                                backgroundColor: '#2563eb',
                                color: 'white',
                                padding: '0.5rem 0.75rem',
                                borderRadius: '0.375rem',
                                fontWeight: '600',
                                border: 'none',
                                cursor: 'pointer'
                            }}
                        >
                            Sign in
                        </button>
                    </form>

                    <div style={{ marginTop: '1.5rem', textAlign: 'center' }}>
                        <span style={{ color: '#6b7280', fontSize: '0.875rem' }}>
                            Don't have an account?
                        </span>
                        <button
                            onClick={() => window.location.href = '/register'}
                            style={{
                                width: '100%',
                                backgroundColor: '#e5e7eb',
                                color: '#111827',
                                padding: '0.5rem 0.75rem',
                                borderRadius: '0.375rem',
                                fontWeight: '600',
                                border: 'none',
                                cursor: 'pointer',
                                marginTop: '0.5rem'
                            }}
                        >
                            Sign up
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default LoginPage;