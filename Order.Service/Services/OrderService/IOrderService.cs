using Order.Data.Entities.OrderEntities;
using Order.Service.Services.OrderService.Dto;

namespace Order.Service.Services.OrderService
{
    public interface IOrderService
    {
        Task<OrderResultDto> CreateNewOrderAsync(List<OrderDto> input, string buyerEmail);
        Task<OrderResultDto> GetDetailsOfSpecificOrderAsync(Guid? id);
        Task<IReadOnlyList<OrderResultDto>> GetAllOrdersDetailsAsync();
        Task<OrderStatusResultDto> UpdateOrderStatusAsync(Guid? id, OrderPayementStatus status);
    }
}
