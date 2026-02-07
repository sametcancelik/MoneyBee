using Microsoft.EntityFrameworkCore;
using MoneyBee.Shared.Enums;
using MoneyBee.Transfer.Application.Interfaces.Persistance;
using MoneyBee.Transfer.Domain.Entities;

namespace MoneyBee.Transfer.Infrastructure.Persistence;

public class TransferDbContext(DbContextOptions<TransferDbContext> options) : DbContext(options), ITransferDbContext
{
    public DbSet<Domain.Entities.Transfer> Transfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransferDbContext).Assembly);

        modelBuilder.Entity<Domain.Entities.Transfer>().HasData(
            new Domain.Entities.Transfer
            {
                Id = Guid.Parse("b11111e9-2ba9-473a-a40f-e38cb54f9b35"),
                SenderCustomerId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                ReceiverCustomerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                Amount = 100.00m,
                Currency = "USD",
                AmountInTry = 3300.00m,
                Fee = 1.50m,
                TransactionCode = "TX-USD-2026-001",
                Status = TransactionStatus.COMPLETED,
                ApprovedDate = DateTime.UtcNow,
                CreatedBy = "System",
                CreatedDate = DateTime.UtcNow
            },
            new Domain.Entities.Transfer
            {
                Id = Guid.Parse("c22222e9-2ba9-473a-a40f-e38cb54f9b35"),
                SenderCustomerId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                ReceiverCustomerId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                Amount = 500.00m,
                Currency = "TRY",
                AmountInTry = 500.00m,
                Fee = 5.00m,
                TransactionCode = "TX-TRY-2026-002",
                Status = TransactionStatus.PENDING,
                CreatedBy = "System",
                CreatedDate = DateTime.UtcNow
            }
        );
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}