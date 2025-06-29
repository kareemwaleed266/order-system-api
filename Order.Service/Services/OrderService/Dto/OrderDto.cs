using System.ComponentModel.DataAnnotations;

namespace Order.Service.Services.OrderService.Dto
{
    public class OrderDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [EmailAddress]
        public string BuyerEmail { get; set; }
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
