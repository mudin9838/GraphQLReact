// src/hooks/useGetProductById.ts

import { useQuery } from "@tanstack/react-query";
import { graphqlClient } from "../graphql/graphqlClient";
import { GET_PRODUCT_BY_ID_QUERY } from "../graphql/queries";
import { Product } from "../types";

export function useGetProductById(id: string) {
  return useQuery({
    queryKey: ["product", id],
    queryFn: async () => {
      const data = await graphqlClient.request(GET_PRODUCT_BY_ID_QUERY, { id });
      return data.getProductById as Product;
    },
    enabled: !!id,
  });
}
