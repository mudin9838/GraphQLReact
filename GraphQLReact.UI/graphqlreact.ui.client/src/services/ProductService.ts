import { getApiEndpointForRoleAndController } from './endpointMapping';
import { Product, ProductInputDto } from '../types/types';

// Function to build headers with optional authentication token
const buildHeaders = (token?: string) => ({
  'Content-Type': 'application/json',
  ...(token && { Authorization: `Bearer ${token}` }), // Add Authorization header only if token is provided
});

// Fetch products based on roles and token
export const fetchProducts = async (roles: string[], token: string): Promise<Product[]> => {
  try {
    const endpoint = getApiEndpointForRoleAndController(roles, 'Products');
    console.log('Fetching products from:', endpoint); // log to verify endpoint
    const response = await fetch(endpoint, {
      method: 'GET',
      headers: buildHeaders(token),
    });
    console.log('Request Headers:', {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    }); // Log headers
    // Check for HTTP error responses
    if (!response.ok) {
      if (response.status === 403) {
          // Handle 403 Forbidden status
          window.location.href = '/unauthorized'; // Redirect to unauthorized page
          return [];
      }
      const errorText = await response.text();
      console.error('Error response:', errorText); // Log the error response
      throw new Error(`Failed to fetch products: ${response.status} ${errorText}`);
    }

    const data = await response.json();
    console.log('Fetched products successfully:', data); // Log the fetched products
    return data;
  } catch (error) {
    console.error('Fetch error:', error.message);
    throw new Error(error.message || 'An error occurred while fetching products.');
  }
};
// Delete a product if the user has the Admin role
export const deleteProduct = async (id: number, roles: string[], token: string): Promise<void> => {
  try {
    if (!roles.includes('Admin')) {
      throw new Error('Unauthorized');
    }

    const endpoint = getApiEndpointForRoleAndController(roles, 'Products');
    const response = await fetch(`${endpoint}/${id}`, {
      method: 'DELETE',
      headers: buildHeaders(token),
    });

    if (!response.ok) {
      throw new Error('Failed to delete product');
    }
  } catch (error) {
    throw new Error(error.message || 'An error occurred while deleting the product.');
  }
};

// Create a new product with authentication token
export const createProduct = async (product: ProductInputDto, token: string): Promise<Product> => {
  try {
    const endpoint = getApiEndpointForRoleAndController([], 'Products');
    const response = await fetch(endpoint, {
      method: 'POST',
      headers: buildHeaders(token),
      body: JSON.stringify(product),
    });

    if (!response.ok) {
      throw new Error('Failed to create product');
    }

    return await response.json();
  } catch (error) {
    throw new Error(error.message || 'An error occurred while creating the product.');
  }
};

// Update an existing product with authentication token
export const updateProduct = async (product: ProductInputDto, token: string): Promise<Product> => {
  try {
    if (!product.id) {
      throw new Error('Product ID is required for update');
    }

    const endpoint = getApiEndpointForRoleAndController([], 'Products');
    const response = await fetch(`${endpoint}/${product.id}`, {
      method: 'PUT',
      headers: buildHeaders(token),
      body: JSON.stringify(product),
    });

    if (!response.ok) {
      throw new Error('Failed to update product');
    }

    return await response.json();
  } catch (error) {
    throw new Error(error.message || 'An error occurred while updating the product.');
  }
};
