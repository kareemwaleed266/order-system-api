namespace Order.Data.Entities.OrderEntities
{
    public class OrderItems : BaseEntity<Guid>
    {
        public Guid OrdersId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductItemOrdered itemOrdered { get; set; }
    }
}