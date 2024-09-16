// import { useQuery } from '@tanstack/react-query';
// import { gql } from '@apollo/client';
// import client from '../graphql/graphqlClient';
// import { GET_PRODUCTS } from '../graphql/queries';
// import { ProductApiResponse } from '../types/types';

// export const fetchProducts = async ({
//   queryKey,
// }: {
//   queryKey: [string, any, any, any, any];
// }) => {
//   const [, columnFilters, globalFilter, sorting, pagination] = queryKey;

//   const { data } = await client.query<ProductApiResponse>({
//     query: GET_PRODUCTS,
//     variables: {
//       first: pagination.pageSize,
//       order: sorting,
//       where: {
//         // Apply filters based on columnFilters and globalFilter
//       },
//     },
//   });
//   return data;
// };
