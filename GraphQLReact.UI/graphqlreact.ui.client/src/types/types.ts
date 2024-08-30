// src/types/types.ts

// Category type with id as number
export type Category = {
  id: number; // id as number
  title: string;
};

// Product type with id as number
export type Product = {
  id: number; // id as number
  title: string;
  description: string; // description is required
  price: number;
  imageUrl: string;
  category: Category;
};

// ProductInputDto type for input
export type ProductInputDto = {
  id?: number; // id is optional for creation but required for updates
  title: string;
  description: string; // description is required
  price: number;
  imageUrl?: string;
  categoryId: number; // Assuming categoryId is a number
};

// CategoryDtoInput type for input
export type CategoryDtoInput = {
  id: number; // ID is required
  title: string;
};

// Response type for the mutation
export type updateCategoryResponse = {
  updateCategory: {
    id: number;
    title: string;
  };
};

// Variables type for the mutation
export type updateCategoryVariables = {
  category: CategoryDtoInput;
};
