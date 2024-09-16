import React, { createContext, useState, useEffect, useContext, ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';
import { getBaseUrl } from '../config/apiConfig';
import { makeRequest } from '../services/baseService';

// Define the shape of the authentication response
interface AuthResponse {
  token: string;
  refreshToken: string;
  userName: string;
  roles: string[];
  profileImageUrl: string;
  succeeded: boolean;
  error?: string;
}

// Define the shape of the AuthContext
interface AuthContextType {
  isAuthenticated: boolean;
  userName: string | null;
  roles: string[];
  profileImageUrl: string | null;
  login: (username: string, password: string) => Promise<void>;
  logout: () => void;
}

// Create the AuthContext with default value as undefined
const AuthContext = createContext<AuthContextType | undefined>(undefined);

// Define the AuthProvider component
export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [userName, setUserName] = useState<string | null>(null);
  const [roles, setRoles] = useState<string[]>([]);
  const [profileImageUrl, setProfileImageUrl] = useState<string | null>(null);
  const navigate = useNavigate(); // Initialize navigate

  // Effect to load authentication data from localStorage on component mount
  useEffect(() => {
    const loadAuthData = () => {
      const token = localStorage.getItem('token');
      if (token) {
        setIsAuthenticated(true);
        setUserName(localStorage.getItem('userName'));
        setRoles(JSON.parse(localStorage.getItem('roles') || '[]'));
        setProfileImageUrl(localStorage.getItem('profileImageUrl'));
      } else {
        setIsAuthenticated(false);
        setUserName(null);
        setRoles([]);
        setProfileImageUrl(null);
      }
    };

    loadAuthData();
  }, []);

  // Function to handle user login
  const login = async (username: string, password: string) => {
    try {
      const response = await makeRequest<AuthResponse>(
        `${getBaseUrl()}/Auth/login`,
        'POST',
        { username, password }
      );

      if (response.succeeded) {
        // Store authentication data in localStorage
        localStorage.setItem('token', response.token);
        localStorage.setItem('refreshToken', response.refreshToken);
        localStorage.setItem('userName', response.userName);
        localStorage.setItem('roles', JSON.stringify(response.roles));
        localStorage.setItem('profileImageUrl', response.profileImageUrl);

        // Update state and redirect
        setIsAuthenticated(true);
        setUserName(response.userName);
        setRoles(response.roles);
        setProfileImageUrl(response.profileImageUrl);

        console.log('Login successful, redirecting to /home');
        navigate('/home'); // Use navigate for redirection
      } else {
        throw new Error(response.error || 'Login failed');
      }
    } catch (error) {
      console.error('Login error:', error);
      throw error;
    }
  };

  // Function to handle user logout
  const logout = () => {
    // Clear authentication data from localStorage
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('userName');
    localStorage.removeItem('roles');
    localStorage.removeItem('profileImageUrl');

    // Update state and redirect
    setIsAuthenticated(false);
    setUserName(null);
    setRoles([]);
    setProfileImageUrl(null);

    navigate('/login');
  };

  // Provide authentication context to children
  return (
    <AuthContext.Provider value={{ isAuthenticated, userName, roles, profileImageUrl, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

// Custom hook to use the authentication context
export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
