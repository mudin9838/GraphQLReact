import { useQuery } from '@tanstack/react-query';
import { graphqlClient } from '../graphql/graphqlClient';
import { GET_PRODUCTS_QUERY } from '../graphql/queries';


export function useGetProducts() {
  return useQuery({
    queryKey: ['products'],
    queryFn: async () => {
      const data = await graphqlClient.request(GET_PRODUCTS_QUERY);
      return data.products;
    },
  });
}
