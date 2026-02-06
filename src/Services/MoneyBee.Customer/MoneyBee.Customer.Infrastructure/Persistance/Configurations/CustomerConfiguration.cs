using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = MoneyBee.Customer.Domain.Entities;

namespace MoneyBee.Customer.Infrastructure.Persistence.Configurations;
public class CustomerConfiguration : IEntityTypeConfiguration<Entity.Customer>
{
    public void Configure(EntityTypeBuilder<Entity.Customer> builder)
    {
        builder.HasIndex(e => e.NationalId).IsUnique();
        builder.Property(e => e.NationalId).HasMaxLength(11).IsRequired();
        builder.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(e => e.LastName).HasMaxLength(50).IsRequired();
        
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}