namespace Order.Data.Entities.OrderEntities
{
    public enum OrderPayementStatus
    {
        Pending,
        Received,
        Failed
    }
    public class Orders : BaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public IReadOnlyList<OrderItems> OrderItems { get; set; }
        public string? PaymentIntentId { get; set; }
        public OrderPayementStatus OrderPayementStatus { get; set; } = OrderPayementStatus.Pending;
        public string Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
