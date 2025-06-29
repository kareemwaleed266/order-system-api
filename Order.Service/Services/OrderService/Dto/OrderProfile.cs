using AutoMapper;
using Order.Data.Entities.OrderEntities;

namespace Order.Service.Services.OrderService.Dto
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Orders, OrderDto>().ReverseMap();
            CreateMap<OrderItems, OrderItemDto>()
                .ForMember(dest => dest.ProductItemId, options => options.MapFrom(src => src.itemOrdered.ProductItemId))
                .ForMember(dest => dest.ProductItemName, options => options.MapFrom(src => src.itemOrdered.ProductItemName)).ReverseMap();
            CreateMap<OrderResultDto, Orders>().ReverseMap();
            CreateMap<OrderItems, OrderResultDto>()
                .ForMember(dest => dest.ProductItemId, options => options.MapFrom(src => src.itemOrdered.ProductItemId))
                .ForMember(dest => dest.ProductItemName, options => options.MapFrom(src => src.itemOrdered.ProductItemName))
                .ForMember(dest => dest.Quantity, options => options.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, options => options.MapFrom(src => src.UnitPrice))
                .ReverseMap();

            CreateMap<Orders, OrderStatusResultDto>().ReverseMap();
        }
    }
}
