using Order.Service.Services.OrderService.Dto;

namespace Order.Service.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<OrderBasketDto> CreateOrUpdatePaymentIntentForExistingOrder(OrderBasketDto input);
        Task<OrderBasketDto> CreateOrUpdatePaymentIntentForNewOrder(Guid orderId);
        Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
