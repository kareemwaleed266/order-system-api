using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Data.Entities.OrderEntities;

namespace Order.Data.Configurations
{
    public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.OwnsOne(orderItem => orderItem.itemOrdered, x =>
            {
                x.WithOwner();
            });
        }
    }
}
