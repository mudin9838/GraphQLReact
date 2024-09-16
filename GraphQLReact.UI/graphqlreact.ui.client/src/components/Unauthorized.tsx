import React from 'react';
import { Container, Alert, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

const Unauthorized: React.FC = () => {
  const navigate = useNavigate();

  return (
    <Container className="text-center mt-5">
      <Alert variant="danger">
        <Alert.Heading>Unauthorized Access</Alert.Heading>
        <p>You do not have permission to view this page.</p>
        <Button onClick={() => navigate('/')} variant="outline-danger">
          Go to Home
        </Button>
      </Alert>
    </Container>
  );
};

export default Unauthorized;
