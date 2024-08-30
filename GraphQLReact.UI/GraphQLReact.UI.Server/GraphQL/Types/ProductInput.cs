namespace GraphQLReact.UI.Server.GraphQL.Inputs;

// This class represents the input type for creating or updating a product.
public class ProductInput
{
    // The ID of the product (for updates). This property should be nullable.
    public int? Id { get; set; }

    // The title of the product (required for create and update operations).
    public string Title { get; set; }

    // The description of the product (optional).
    public string Description { get; set; }

    // The price of the product (required).
    public decimal Price { get; set; }

    // The URL of the product's image (optional).
    public string ImageUrl { get; set; }

    // The ID of the category to which the product belongs (required).
    public int CategoryId { get; set; }
}

// GraphQL input type configuration for ProductInput.
public class ProductInputType : InputObjectType<ProductInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<ProductInput> descriptor)
    {
        descriptor.Field(p => p.Id).Type<IdType>().Description("The ID of the product.");
        descriptor.Field(p => p.Title).Type<NonNullType<StringType>>().Description("The title of the product.");
        descriptor.Field(p => p.Description).Type<StringType>().Description("The description of the product.");
        descriptor.Field(p => p.Price).Type<NonNullType<DecimalType>>().Description("The price of the product.");
        descriptor.Field(p => p.ImageUrl).Type<StringType>().Description("The URL of the product's image.");
        descriptor.Field(p => p.CategoryId).Type<NonNullType<IntType>>().Description("The ID of the category to which the product belongs.");
    }
}
