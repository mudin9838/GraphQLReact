using AutoMapper;
using GraphQLReact.BLL.Dtos;
using GraphQLReact.DLL.Entities;

namespace GraphQLReact.BLL.Helper;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Product to ProductDto mapping
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

        // ProductDto to Product mapping
        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.Category, opt => opt.Ignore());

        // Product to ProductDto mapping
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        // ProductDto to Product mapping
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());


    }
}