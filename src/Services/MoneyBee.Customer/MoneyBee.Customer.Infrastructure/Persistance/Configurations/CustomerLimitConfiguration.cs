using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = MoneyBee.Customer.Domain.Entities;

namespace MoneyBee.Customer.Infrastructure.Persistence.Configurations;
public class CustomerLimitConfiguration : IEntityTypeConfiguration<Entity.CustomerLimit>
{
    public void Configure(EntityTypeBuilder<Entity.CustomerLimit> builder)
    {
        builder.Property(e => e.DailyTotalAmount).HasPrecision(18, 2);
            
        builder.HasOne<Entity.Customer>()
              .WithOne()
              .HasForeignKey<Entity.CustomerLimit>(e => e.CustomerId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}