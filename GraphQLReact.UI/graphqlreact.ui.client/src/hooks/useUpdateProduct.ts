import { useMutation, useQueryClient } from '@tanstack/react-query';
import { graphqlClient } from '../graphql/graphqlClient';
import { UPDATE_PRODUCT_MUTATION } from '../graphql/mutation';


export function useUpdateProduct() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (product: any) => {
      const data = await graphqlClient.request(UPDATE_PRODUCT_MUTATION, {
        product,
      });
      return data.updateProduct;
    },
    onSuccess: () => {
      queryClient.invalidateQueries(['products']);
    },
  });
}
