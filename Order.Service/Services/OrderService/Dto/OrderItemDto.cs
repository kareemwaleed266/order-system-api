namespace Order.Service.Services.OrderService.Dto
{
    public class OrderItemDto
    {
        public Guid OrdersId { get; set; }
        public int ProductItemId { get; set; }
        public string ProductItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
