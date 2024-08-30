import { useMutation, useQueryClient } from "@tanstack/react-query";
import { CREATE_PRODUCT_MUTATION } from "../graphql/mutation";
import { graphqlClient } from "../graphql/graphqlClient";


export function useCreateProduct() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (product: any) => {
      const data = await graphqlClient.request(CREATE_PRODUCT_MUTATION, {
        product,
      });
      return data.createProduct;
    },
    onSuccess: () => {
      queryClient.invalidateQueries(["products"]);
    },
  });
}
