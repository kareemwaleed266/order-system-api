using Order.Data.Entities.OrderEntities;

namespace Order.Service.Services.OrderService.Dto
{
    public class OrderStatusResultDto
    {
        public Guid Id { get; set; }
        public OrderPayementStatus OrderPayementStatus { get; set; } = OrderPayementStatus.Pending;
        public DateTimeOffset OrderDate { get; set; }

    }
}
