using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = MoneyBee.Customer.Domain.Entities;

namespace MoneyBee.Customer.Infrastructure.Persistence.Configurations;
public class AccountConfiguration : IEntityTypeConfiguration<Entity.Account>
{
    public void Configure(EntityTypeBuilder<Entity.Account> builder)
    {
        builder.HasIndex(e => e.AccountNumber).IsUnique();
        builder.Property(e => e.Balance).HasPrecision(18, 2);
        builder.Property(e => e.Currency).HasMaxLength(3).IsRequired();
            
        builder.HasOne<Customer.Domain.Entities.Customer>()
              .WithMany()
              .HasForeignKey(e => e.CustomerId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}