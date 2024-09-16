namespace GraphQLReact.BLL.Interfaces;
public interface IUserContextService
{
    string GetUserId();
    bool IsAdmin();
}