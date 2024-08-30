import { useQuery } from '@tanstack/react-query';
import { GET_CATEGORIES_QUERY } from '../graphql/queries';
import { graphqlClient } from '../graphql/graphqlClient';


export function useGetCategories() {
  return useQuery({
    queryKey: ['categories'],
    queryFn: async () => {
      const data = await graphqlClient.request(GET_CATEGORIES_QUERY);
      return data.categories;
    },
  });
}
