namespace GraphQLReact.BLL.Dtos;
public class ProductBaseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
}
