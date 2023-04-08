using AutoMapper;
using IdentityApp.DTOs.ProductDto;

namespace IdentityApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductRequest>();
            CreateMap<ProductRequest, Product>();

            CreateMap<Product, ProductResponse>();
            CreateMap<ProductResponse, Product>();
        }
    }
}
