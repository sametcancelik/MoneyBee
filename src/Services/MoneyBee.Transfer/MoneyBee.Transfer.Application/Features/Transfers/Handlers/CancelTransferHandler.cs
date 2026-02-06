using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyBee.Shared.Enums;
using MoneyBee.Transfer.Application.Features.Transfers.Commands;
using MoneyBee.Transfer.Application.Interfaces.Persistance;

namespace MoneyBee.Transfer.Application.Features.Transfers.Handlers;

public class CancelTransferHandler(ITransferDbContext _context) : IRequestHandler<CancelTransferCommand, bool>
{
    public async Task<bool> Handle(CancelTransferCommand request, CancellationToken cancellationToken)
    {
        var transfer = await _context.Transfers
            .FirstOrDefaultAsync(x => x.TransactionCode == request.TransactionCode, cancellationToken);

        if (transfer == null)
            throw new Exception("İşlem bulunamadı.");

        if (transfer.Status == TransactionStatus.COMPLETED)
            throw new Exception("Tamamlanmış bir işlem iptal edilemez.");

        if (transfer.Status == TransactionStatus.CANCELLED)
            throw new Exception("Bu işlem zaten iptal edilmiş.");

        transfer.Status = TransactionStatus.CANCELLED;
        
        transfer.Fee = 0; 

        transfer.UpdatedDate = DateTime.UtcNow;
        transfer.UpdatedBy = "Customer_Request";

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}