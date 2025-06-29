using Order.Service.Services.CustomerService.Dto;
using Order.Service.Services.OrderService.Dto;

namespace Order.Service.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<CustomerResultDto> CreateNewCustomerAsync(CustomerDto input);
        Task<IReadOnlyList<OrderResultDto>> GetAllOrdersForCustomerAsync(Guid? id);
    }
}
