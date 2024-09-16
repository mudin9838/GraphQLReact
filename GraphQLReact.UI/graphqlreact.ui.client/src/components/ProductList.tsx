// src/components/ProductList.tsx

import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Table, Alert, Button, Spinner } from 'react-bootstrap';
import { Product } from '../types/types';
import { fetchProducts, deleteProduct } from '../services/ProductService';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const ProductList: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const { roles } = useAuth();
  const token = localStorage.getItem('token') || '';
  const navigate = useNavigate();

  // Fetch products when roles or token change
    // Debugging statements
    useEffect(() => {
      console.log('Current roles:', roles);
      console.log('Current token:', token);
    const fetchData = async () => {
      setIsLoading(true);
      setError(null);
      try {
        const data = await fetchProducts(roles, token);
        console.log('Fetched products:', data);
        setProducts(data);
      } catch (error) {
        console.error('Error fetching products:', error);
        if (error.message.includes('Access denied')) {
          navigate('/unauthorized'); // Redirect to unauthorized page
        } else {
          setError(error.message || 'An error occurred while fetching products.');
        }
      } finally {
        setIsLoading(false);
      }
    };

    fetchData();
  }, [roles, token, navigate]);

  // Handle product deletion
  const handleDelete = async (id: number) => {
    try {
      await deleteProduct(id, roles, token);
      setProducts(prevProducts => prevProducts.filter(product => product.id !== id));
    } catch (error) {
      setError(error.message || 'An error occurred while deleting the product.');
    }
  };

  // Display loading spinner, error message, or product table
  if (isLoading) {
    return (
      <Container className="text-center" style={{ height: '100vh' }} fluid>
        <Spinner animation="border" role="status">
          <span className="visually-hidden">Loading...</span>
        </Spinner>
      </Container>
    );
  }

  return (
    <Container fluid className="p-3">
      {error && <Alert variant="danger">{error}</Alert>}
      <Row>
        <Col>
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>ID</th>
                <th>Title</th>
                <th>Description</th>
                <th>Price</th>
                <th>Image URL</th>
                <th>Category</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {products.map(product => (
                <tr key={product.id}>
                  <td>{product.id}</td>
                  <td>{product.title}</td>
                  <td>{product.description}</td>
                  <td>{product.price}</td>
                  <td>{product.imageUrl}</td>
                  <td>{product.category?.title}</td>
                  <td>
                    <Button
                      variant="danger"
                      onClick={() => handleDelete(product.id)}
                      disabled={isLoading} // Disable button while loading
                    >
                      Delete
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        </Col>
      </Row>
    </Container>
  );
};

export default ProductList;