export const GET_PRODUCTS_QUERY = `
  query {
    products {
      id
      title
      description
      price
      imageUrl
      category {
        id
        title
      }
    }
  }
`;

export const GET_CATEGORIES_QUERY = `
  query {
    categories {
      id
      title
    }
  }
`;

export const GET_PRODUCT_BY_ID_QUERY = `
  query GetProductById($id: ID!) {
    getProductById(id: $id) {
      id
      title
      description
      price
      imageUrl
      category {
        id
        title
      }
    }
  }
`;

export const GET_CATEGORY_BY_ID_QUERY = `
  query GetCategoryById($id: ID!) {
    getCategoryById(id: $id) {
      id
      title
    }
  }
`;
