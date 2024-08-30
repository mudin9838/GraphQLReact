import { useMutation, useQueryClient } from "@tanstack/react-query";
import { graphqlClient } from "../graphql/graphqlClient";
import { UPDATE_CATEGORY_MUTATION } from "../graphql/mutation";
import { CategoryDtoInput, updateCategoryResponse } from "../types/types";

export function useUpdateCategory() {
  const queryClient = useQueryClient();

  return useMutation<updateCategoryResponse, Error, CategoryDtoInput>({
    mutationFn: async (category: CategoryDtoInput) => {
      console.log("Sending mutation with data:", { categoryDto: category }); // Debug output

      try {
        const response = await graphqlClient.request<updateCategoryResponse>(
          UPDATE_CATEGORY_MUTATION,
          {
            categoryDto: category, // Ensure this matches the mutation's expected variable name
          }
        );
        return response.updateCategory;
      } catch (error) {
        console.error("Error updating category:", error);
        throw error;
      }
    },
    onSuccess: () => {
      queryClient.invalidateQueries(["categories"]);
    },
    onError: (error) => {
      console.error("Error updating category:", error);
    },
  });
}
