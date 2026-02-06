using Microsoft.EntityFrameworkCore;
using MoneyBee.Transfer.Application.Interfaces;
using MoneyBee.Transfer.Application.Interfaces.Persistance;
using MoneyBee.Transfer.Domain.Entities;

namespace MoneyBee.Transfer.Infrastructure.Persistence;

public class TransferDbContext(DbContextOptions<TransferDbContext> options) : DbContext(options), ITransferDbContext
{
    public DbSet<Domain.Entities.Transfer> Transfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransferDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}