using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyBee.Shared.Enums;
using MoneyBee.Shared.Exceptions;
using MoneyBee.Shared.Models;
using MoneyBee.Transfer.Application.Features.Transfers.Commands;
using MoneyBee.Transfer.Application.Interfaces.Persistance;

namespace MoneyBee.Transfer.Application.Features.Transfers.Handlers;

public class CancelTransferHandler(ITransferDbContext context) : IRequestHandler<CancelTransferCommand, ServiceResponse<bool>>
{
    public async Task<ServiceResponse<bool>> Handle(CancelTransferCommand request, CancellationToken cancellationToken)
    {
        var transfer = await context.Transfers
            .FirstOrDefaultAsync(x => x.TransactionCode == request.TransactionCode, cancellationToken) 
            ?? throw new BusinessException("Transfer bulunamadı.", 404);

        if (transfer.Status == TransactionStatus.COMPLETED)
        {
            throw new BusinessException("Tamamlanmış bir işlem iptal edilemez.");
        }

        if (transfer.Status == TransactionStatus.CANCELLED)
        {
            throw new BusinessException("Bu işlem zaten iptal edilmiş.");
        }

        transfer.Status = TransactionStatus.CANCELLED;
        transfer.Fee = 0m;
        transfer.UpdatedDate = DateTime.UtcNow;
        transfer.UpdatedBy = "Customer_Request";

        await context.SaveChangesAsync(cancellationToken);

        return ServiceResponse<bool>.Success(true);
    }
}