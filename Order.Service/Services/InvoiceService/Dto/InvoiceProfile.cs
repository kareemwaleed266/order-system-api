using AutoMapper;
using Order.Data.Entities;

namespace Order.Service.Services.InvoiceService.Dto
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<InvoiceResultDto, Invoice>().ReverseMap();
        }
    }
}
