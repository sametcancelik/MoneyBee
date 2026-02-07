using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MoneyBee.Customer.Application.Interfaces.Persistance;
using MoneyBee.Customer.Domain.Entities;
using MoneyBee.Shared.Entities;
using MoneyBee.Shared.Enums;

namespace MoneyBee.Customer.Infrastructure.Persistence;

public class CustomerDbContext : DbContext, ICustomerDbContext
{
	public DbSet<MoneyBee.Customer.Domain.Entities.Customer> Customers => Set<MoneyBee.Customer.Domain.Entities.Customer>();

	public DbSet<Account> Accounts => Set<Account>();

	public DbSet<CustomerLimit> CustomerLimits => Set<CustomerLimit>();

	public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<MoneyBee.Customer.Domain.Entities.Customer>().HasData(new MoneyBee.Customer.Domain.Entities.Customer
		{
			Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
			FirstName = "Samet Can",
			LastName = "Çelik",
			NationalId = "12345678901",
			PhoneNumber = "5551112233",
			Email = "samet@moneybee.com",
			BirthDate = new DateTime(1995, 1, 1, 0, 0, 0, DateTimeKind.Utc),
			Type = CustomerType.Individual,
			IsKycVerified = true,
			Status = CustomerStatus.Active,
			CreatedBy = "System",
			CreatedDate = DateTime.UtcNow
		}, new MoneyBee.Customer.Domain.Entities.Customer
		{
			Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
			FirstName = "Viewer",
			LastName = "User",
			NationalId = "98765432109",
			PhoneNumber = "5559998877",
			Email = "viewer@moneybee.com",
			BirthDate = new DateTime(1988, 5, 20, 0, 0, 0, DateTimeKind.Utc),
			Type = CustomerType.Corporate,
			TaxNumber = "9998887766",
			IsKycVerified = false,
			Status = CustomerStatus.Active,
			CreatedBy = "System",
			CreatedDate = DateTime.UtcNow
		});
		modelBuilder.Entity<Account>().HasData(new Account
		{
			Id = Guid.Parse("e11111e9-2ba9-473a-a40f-e38cb54f9b35"),
			CustomerId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
			AccountNumber = "TR990000100012345678901001",
			Balance = 10000.00m,
			Currency = "TRY",
			IsActive = true,
			CreatedBy = "System",
			CreatedDate = DateTime.UtcNow
		}, new Account
		{
			Id = Guid.Parse("e22222e9-2ba9-473a-a40f-e38cb54f9b35"),
			CustomerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
			AccountNumber = "TR990000100012345678901002",
			Balance = 500.00m,
			Currency = "USD",
			IsActive = true,
			CreatedBy = "System",
			CreatedDate = DateTime.UtcNow
		});
		modelBuilder.Entity<CustomerLimit>().HasData(new CustomerLimit
		{
			Id = Guid.Parse("f11111e9-2ba9-473a-a40f-e38cb54f9b35"),
			CustomerId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
			DailyTotalAmount = 10000.00m,
			LastTransactionDate = DateTime.UtcNow,
			CreatedBy = "System",
			CreatedDate = DateTime.UtcNow
		}, new CustomerLimit
		{
			Id = Guid.Parse("f22222e9-2ba9-473a-a40f-e38cb54f9b35"),
			CustomerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
			DailyTotalAmount = 0.00m,
			LastTransactionDate = DateTime.UtcNow,
			CreatedBy = "System",
			CreatedDate = DateTime.UtcNow
		});
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		foreach (EntityEntry<AuditableEntity> item in ChangeTracker.Entries<AuditableEntity>())
		{
			if (item.State == EntityState.Added)
			{
				item.Entity.CreatedDate = DateTime.UtcNow;
				item.Entity.IsDeleted = false;
			}
			else if (item.State == EntityState.Modified)
			{
				item.Entity.UpdatedDate = DateTime.UtcNow;
			}
			else if (item.State == EntityState.Deleted)
			{
				item.State = EntityState.Modified;
				item.Entity.IsDeleted = true;
				item.Entity.DeletedDate = DateTime.UtcNow;
			}
		}
		return base.SaveChangesAsync(cancellationToken);
	}
}
