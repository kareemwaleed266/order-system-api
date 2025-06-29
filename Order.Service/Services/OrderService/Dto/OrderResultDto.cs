using Order.Data.Entities.OrderEntities;

namespace Order.Service.Services.OrderService.Dto
{
    public class OrderResultDto
    {
        public Guid OrdersId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        //public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        //public string? PaymentIntentId { get; set; }


        public int ProductItemId { get; set; }
        public string ProductItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }


        public OrderPayementStatus OrderPayementStatus { get; set; }
        public string? PaymentIntentId { get; set; }
        public string Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
