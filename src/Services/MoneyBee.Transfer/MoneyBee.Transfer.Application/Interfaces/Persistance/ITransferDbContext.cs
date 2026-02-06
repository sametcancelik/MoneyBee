using Microsoft.EntityFrameworkCore;

namespace MoneyBee.Transfer.Application.Interfaces.Persistance;

public interface ITransferDbContext
{
    DbSet<Domain.Entities.Transfer> Transfers { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
