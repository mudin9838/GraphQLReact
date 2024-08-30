// src/hooks/useCreateCategory.ts
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { graphqlClient } from "../graphql/graphqlClient";
import { CREATE_CATEGORY_MUTATION } from "../graphql/mutation";
import { CategoryDtoInput } from "../types/types"; // Adjust import path as necessary

export function useCreateCategory() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (categoryDtoInput: CategoryDtoInput) => {
      // Perform the GraphQL request with CategoryDtoInput
      const response = await graphqlClient.request<{
        createCategory: CategoryDtoInput; // Expecting CategoryDtoInput as the response type
      }>(CREATE_CATEGORY_MUTATION, {
        addCategories: categoryDtoInput, // Use CategoryDtoInput for input
      });
      return response.createCategory; // Return the created category data
    },
    onSuccess: () => {
      // Invalidate and refetch relevant queries to ensure the UI is updated
      queryClient.invalidateQueries(["categories"]);
    },
    onError: (error) => {
      // Log the error to the console
      console.error("Error creating category:", error);
    },
  });
}
