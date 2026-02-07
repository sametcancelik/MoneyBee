using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyBee.Customer.Domain.Entities;

namespace MoneyBee.Customer.Infrastructure.Persistence.Configurations;

public class CustomerLimitConfiguration : IEntityTypeConfiguration<CustomerLimit>
{
	public void Configure(EntityTypeBuilder<CustomerLimit> builder)
	{
		builder.Property((CustomerLimit e) => e.DailyTotalAmount).HasPrecision(18, 2);
		builder.HasIndex((CustomerLimit e) => e.CustomerId).IsUnique();
	}
}
