using System.Reflection;
using Microsoft.EntityFrameworkCore;
using E = MoneyBee.Customer.Domain.Entities;
using MoneyBee.Shared.Entities;
using MoneyBee.Customer.Application.Interfaces.Persistance;

namespace MoneyBee.Customer.Infrastructure.Persistence;
public class CustomerDbContext : DbContext, ICustomerDbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

    public DbSet<E.Customer> Customers => Set<E.Customer>();
    public DbSet<E.Account> Accounts => Set<E.Account>();
    public DbSet<E.CustomerLimit> CustomerLimits => Set<E.CustomerLimit>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.UtcNow;
                entry.Entity.IsDeleted = false;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedDate = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedDate = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}