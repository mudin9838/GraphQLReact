using AuthPlus.Identity.Dtos;
using AuthPlus.Identity.Entities;
using AutoMapper;
using GraphQLReact.BLL.Dtos;
using GraphQLReact.DLL.Entities;

namespace GraphQLReact.BLL.Helper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Mapping from Product to ProductDto
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category)); // Map Category

        // Mapping from ProductBaseDto to Product
        CreateMap<ProductBaseDto, Product>()
            .ForMember(dest => dest.Category, opt => opt.Ignore()) // Ignore the Category property during mapping
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId)); // Map CategoryId

        // Mapping from ProductCreateDto to Product
        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore the Id property during mapping
            .ForMember(dest => dest.Category, opt => opt.Ignore()); // Ignore the Category property during mapping

        // Mapping from ProductUpdateDto to Product
        CreateMap<ProductUpdateDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore the Id property during mapping
            .ForMember(dest => dest.Category, opt => opt.Ignore()); // Ignore the Category property during mapping

        // Mapping from Category to CategoryDto without Products collection
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Products, opt => opt.Ignore()); // Ignore Products

        // Mapping from CategoryDto to Category
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Products, opt => opt.Ignore()); // Ignore Products when mapping from CategoryDto


        // ApplicationUser to UserDto mapping
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed))
            .ForMember(dest => dest.Roles, opt => opt.Ignore())
            .ForMember(dest => dest.CurrentPassword, opt => opt.Ignore())
            .ForMember(dest => dest.NewPassword, opt => opt.Ignore())
            .ForMember(dest => dest.ConfirmNewPassword, opt => opt.Ignore());

        // UserDto to ApplicationUser mapping
        CreateMap<UserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.ProfileImageUrl))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed));

        // UserProduct to ProductDto mapping, including Category
        CreateMap<UserProduct, ProductDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Product.Category)); // Keep it for completeness
    }
}