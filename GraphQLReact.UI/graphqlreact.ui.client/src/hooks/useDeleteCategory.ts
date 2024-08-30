// src/hooks/useDeleteCategory.ts

import { useMutation, useQueryClient } from '@tanstack/react-query';
import { DELETE_CATEGORY_MUTATION } from '../graphql/mutation';
import { graphqlClient } from '../graphql/graphqlClient';

export function useDeleteCategory() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (id: number) => {
      // Ensure `id` is passed as an integer, matching the mutation signature
      await graphqlClient.request(DELETE_CATEGORY_MUTATION, { id });
    },
    onSuccess: () => {
      queryClient.invalidateQueries(['categories']); // Refresh the categories query to reflect the changes
    },
    onError: (error) => {
      console.error('Error deleting category:', error); // Handle any errors
    },
  });
}
