using AutoMapper;
using Order.Data.Entities.ProductEntities;

namespace Order.Service.Services.ProductService.ProductDto
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDto>().ReverseMap();
            CreateMap<Product, ProductDetailsDto>().ReverseMap();
        }
    }
}
