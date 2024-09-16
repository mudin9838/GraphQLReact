import React from 'react';
import { Routes, Route, Navigate, useNavigate } from 'react-router-dom';
import { Container, Navbar, Nav, Button } from 'react-bootstrap';
import LoginForm from './components/LoginForm';
import PrivateRoute from './components/PrivateRoute';
import ProductList from './components/ProductList';
import Unauthorized from './components/Unauthorized';
import { useAuth } from './contexts/AuthContext';
import 'bootstrap/dist/css/bootstrap.min.css';
import Home from './components/Home';

const App: React.FC = () => {
  const { isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();

  console.log('App isAuthenticated:', isAuthenticated); // Debugging line

  return (
    <div className="App">
      <Navbar bg="dark" variant="dark" expand="lg">
        <Navbar.Brand href="#">My App</Navbar.Brand>
        {isAuthenticated && (
          <>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
              <Nav className="me-auto">
              <Nav.Link as={Button} variant="outline-light" onClick={() => navigate('/home')}>
                  Home
                </Nav.Link>
                <Nav.Link as={Button} variant="outline-light" onClick={() => navigate('/products')}>
                  Products
                </Nav.Link>
                
                <Nav.Link as={Button} variant="outline-light" onClick={logout} className="ml-2">
                  Logout
                </Nav.Link>

              </Nav>
            </Navbar.Collapse>
          </>
        )}
      </Navbar>
      <Container className="mt-4">
        <Routes>
          <Route path="/login" element={<LoginForm />} />
          <Route element={<PrivateRoute roles={['Admin', 'User']} />}>
            <Route path="/products" element={<ProductList />} />
            <Route path="/home" element={<Home />} />
          </Route>
          <Route path="/unauthorized" element={<Unauthorized />} /> {/* Add Unauthorized route */}
          <Route path="*" element={<Navigate to={isAuthenticated ? '/home' : '/login'} replace />} />
        </Routes>
      </Container>
    </div>
  );
};

export default App;
