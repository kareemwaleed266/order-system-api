using Order.Data.Entities.OrderEntities;

namespace Order.Service.Services.OrderService.Dto
{
    public class OrderBasketDto
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public IReadOnlyList<OrderItems> OrderItems { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSercret { get; set; }
        //public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
