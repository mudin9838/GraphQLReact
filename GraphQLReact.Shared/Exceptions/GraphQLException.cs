namespace GraphQLReact.Shared.Exceptions;

public class GraphQLException : Exception
{
    public GraphQLException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public GraphQLException(string message)
        : base(message)
    {
    }
}

