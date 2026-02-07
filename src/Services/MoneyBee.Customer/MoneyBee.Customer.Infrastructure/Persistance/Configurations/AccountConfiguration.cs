using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyBee.Customer.Domain.Entities;

namespace MoneyBee.Customer.Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
	public void Configure(EntityTypeBuilder<Account> builder)
	{
		builder.HasIndex((Account e) => e.AccountNumber).IsUnique();
		builder.Property((Account e) => e.Balance).HasPrecision(18, 2);
		builder.Property((Account e) => e.Currency).HasMaxLength(3).IsRequired();
		builder.HasOne<MoneyBee.Customer.Domain.Entities.Customer>().WithMany().HasForeignKey((Account e) => e.CustomerId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
