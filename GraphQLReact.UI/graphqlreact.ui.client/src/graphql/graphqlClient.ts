import { GraphQLClient } from "graphql-request";

const endpoint = "https://localhost:7031/graphql"; // Adjust based on your GraphQL endpoint

export const graphqlClient = new GraphQLClient(endpoint);
