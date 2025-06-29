using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Data.Entities.ProductEntities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
