using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Data.Entities.OrderEntities;

namespace Order.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.HasMany(order => order.OrderItems)
                .WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
