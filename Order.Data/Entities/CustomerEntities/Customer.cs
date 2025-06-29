using Order.Data.Entities.OrderEntities;

namespace Order.Data.Entities.CustomerEntities
{
    public class Customer : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Orders> Orders { get; set; }
    }
}
