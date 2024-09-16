// services/endpointMapping.ts
import { getBaseUrl } from '../config/apiConfig';

const BASE_URL = getBaseUrl();
// Define common endpoint URLs
const BASE_PRODUCTS_URL = `${BASE_URL}/Products`;
//const BASE_ORDERS_URL = `${BASE_URL}/Orders`;

interface EndpointMapping {
  [role: string]: {
    [controller: string]: string;
  };
}

const endpointMapping: EndpointMapping = {
  Admin: {
    Products: BASE_PRODUCTS_URL,
    Orders: `${BASE_URL}/Orders/admin-orders`,
    // Add other controllers for Admin
  },
  User: {
    Products: BASE_PRODUCTS_URL,
    Orders: `${BASE_URL}/Orders/user-orders`,
    // Add other controllers for User
  },
  Manager: {
    Products: `${BASE_URL}/Products/manager-products`,
    Orders: `${BASE_URL}/Orders/manager-orders`,
    // Add other controllers for Manager
  },
  // Add other roles and controllers as needed
};

export const getApiEndpointForRoleAndController = (roles: string[], controller: string): string => {
  // Find the first role with a defined endpoint for the specified controller
  const roleWithEndpoint = roles.find(role => endpointMapping[role]?.[controller]);
  
  // Return the endpoint if found; otherwise, return a default endpoint
  return roleWithEndpoint ? endpointMapping[roleWithEndpoint][controller] : `${BASE_URL}/${controller}/global-products`;
};
