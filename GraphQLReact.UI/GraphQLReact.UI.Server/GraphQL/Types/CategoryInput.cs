namespace GraphQLReact.UI.Server.GraphQL.Inputs;

// This class represents the input type for creating or updating a category.
public class CategoryInput
{
    // The ID of the category (for updates). This property should be nullable.
    public int? Id { get; set; }

    // The title of the category (required for create and update operations).
    public string Title { get; set; }
}

// GraphQL input type configuration for CategoryInput.
public class CategoryInputType : InputObjectType<CategoryInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<CategoryInput> descriptor)
    {
        descriptor.Field(c => c.Id).Type<IdType>().Description("The ID of the category.");
        descriptor.Field(c => c.Title).Type<NonNullType<StringType>>().Description("The title of the category.");
    }
}
