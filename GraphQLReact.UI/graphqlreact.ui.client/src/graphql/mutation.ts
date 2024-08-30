// src/graphql/mutation.ts

export const CREATE_PRODUCT_MUTATION = `
  mutation CreateProduct($product: ProductInput!) {
    createProduct(product: $product) {
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

export const UPDATE_PRODUCT_MUTATION = `
  mutation UpdateProduct($product: ProductInput!) {
    updateProduct(product: $product) {
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

export const DELETE_PRODUCT_MUTATION = `
  mutation DeleteProduct($id: ID!) {
    deleteProduct(id: $id)
  }
`;

export const CREATE_CATEGORY_MUTATION = `
  mutation CreateCategory($addCategories: CategoryDtoInput!) {
    createCategory(categoryDto: $addCategories) {
      id
      title
    }
  }
`;
export const UPDATE_CATEGORY_MUTATION = `
  mutation UpdateCategory($categoryDto: CategoryDtoInput!) {
    updateCategory(categoryDto: $categoryDto) {
      id
      title
    }
  }
`;
export const DELETE_CATEGORY_MUTATION = `
  mutation DeleteCategory($id: Int!) {
    deleteCategory(id: $id)
  }
`;
