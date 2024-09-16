import React from 'react';
import { Outlet, Navigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

interface PrivateRouteProps {
  roles?: string[];
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ roles = [] }) => {
  const { isAuthenticated, roles: userRoles } = useAuth();

  console.log('PrivateRoute isAuthenticated:', isAuthenticated); // Debugging line
  console.log('PrivateRoute User Roles:', userRoles); // Debugging line
  console.log('PrivateRoute Required Roles:', roles); // Debugging line

  // Redirect to login if not authenticated
  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  // Redirect if the user does not have required roles
  if (roles.length && !roles.some(role => userRoles.includes(role))) {
    return <Navigate to="/unauthorized" replace />;
  }

  // Render the child routes
  return <Outlet />;
};

export default PrivateRoute;
