namespace Order.Data.Entities
{
    public class Invoice : BaseEntity<Guid>
    {
        public Guid OrderId { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
    }
}
