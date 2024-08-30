namespace GraphQLReact.DLL.Entities;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
