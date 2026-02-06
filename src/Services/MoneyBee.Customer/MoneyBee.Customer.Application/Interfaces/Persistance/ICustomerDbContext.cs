using Microsoft.EntityFrameworkCore;

using MoneyBee.Customer.Domain.Entities;

namespace MoneyBee.Customer.Application.Interfaces.Persistance;

public interface ICustomerDbContext
{
    DbSet<Domain.Entities.Customer> Customers { get; }
    DbSet<Account> Accounts { get; }
    DbSet<CustomerLimit> CustomerLimits { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
