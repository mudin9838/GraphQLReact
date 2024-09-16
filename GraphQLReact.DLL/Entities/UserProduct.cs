
using AuthPlus.Identity.Entities;

namespace GraphQLReact.DLL.Entities;

public class UserProduct
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}
