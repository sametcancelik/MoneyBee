using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyBee.Customer.Domain.Entities;

namespace MoneyBee.Customer.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<MoneyBee.Customer.Domain.Entities.Customer>
{
	public void Configure(EntityTypeBuilder<MoneyBee.Customer.Domain.Entities.Customer> builder)
	{
		builder.HasIndex((MoneyBee.Customer.Domain.Entities.Customer e) => e.NationalId).IsUnique();
		builder.Property((MoneyBee.Customer.Domain.Entities.Customer e) => e.NationalId).HasMaxLength(11).IsRequired();
		builder.Property((MoneyBee.Customer.Domain.Entities.Customer e) => e.FirstName).HasMaxLength(50).IsRequired();
		builder.Property((MoneyBee.Customer.Domain.Entities.Customer e) => e.LastName).HasMaxLength(50).IsRequired();
		builder.Property((MoneyBee.Customer.Domain.Entities.Customer e) => e.Email).HasMaxLength(50);
		builder.HasQueryFilter((MoneyBee.Customer.Domain.Entities.Customer e) => !e.IsDeleted);
		builder.HasOne((MoneyBee.Customer.Domain.Entities.Customer c) => c.CustomerLimit).WithOne((CustomerLimit cl) => cl.Customer).HasForeignKey((CustomerLimit cl) => cl.CustomerId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.HasQueryFilter((MoneyBee.Customer.Domain.Entities.Customer e) => !e.IsDeleted);
	}
}
