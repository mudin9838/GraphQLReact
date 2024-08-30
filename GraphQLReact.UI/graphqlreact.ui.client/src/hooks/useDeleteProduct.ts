import { useMutation, useQueryClient } from '@tanstack/react-query';
import { DELETE_PRODUCT_MUTATION } from '../graphql/mutation';
import { graphqlClient } from '../graphql/graphqlClient';


export function useDeleteProduct() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (id: string) => {
      await graphqlClient.request(DELETE_PRODUCT_MUTATION, { id });
    },
    onSuccess: () => {
      queryClient.invalidateQueries(['products']);
    },
  });
}
