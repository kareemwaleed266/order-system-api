using AutoMapper;
using Order.Data.Entities.CustomerEntities;

namespace Order.Service.Services.CustomerService.Dto
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<CustomerDto, CustomerResultDto>().ReverseMap();
        }
    }
}
