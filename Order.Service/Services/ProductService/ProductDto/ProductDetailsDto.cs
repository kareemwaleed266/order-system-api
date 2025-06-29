using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Service.Services.ProductService.ProductDto
{
    public class ProductDetailsDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int Stock { get; set; } = 0;
    }
}
