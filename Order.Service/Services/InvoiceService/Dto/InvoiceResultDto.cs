namespace Order.Service.Services.InvoiceService.Dto
{
    public class InvoiceResultDto
    {
        public Guid OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
