namespace GraphQLReact.BLL.Dtos;
public class ProductDto : ProductBaseDto
{
    public int Id { get; set; }

    public CategoryDto? Category { get; set; }
}

