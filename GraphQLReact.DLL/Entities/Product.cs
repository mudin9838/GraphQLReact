namespace GraphQLReact.DLL.Entities;
public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; } // Make this nullable if Category is optional
    public Category? Category { get; set; }

}
