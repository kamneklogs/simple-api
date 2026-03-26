using AutoMapper;
using SimpleApi.Data.Entities;
using SimpleApi.Service.DTOs.Products;

namespace SimpleApi.Service.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, ProductDto>();
    }
}
