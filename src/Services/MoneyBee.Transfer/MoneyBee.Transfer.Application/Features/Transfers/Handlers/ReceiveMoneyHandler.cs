using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyBee.Shared.Enums;
using MoneyBee.Shared.Exceptions;
using MoneyBee.Shared.Models;
using MoneyBee.Transfer.Application.Features.Transfers.Commands;
using MoneyBee.Transfer.Application.Interfaces;
using MoneyBee.Transfer.Application.Interfaces.Persistance;

namespace MoneyBee.Transfer.Application.Features.Transfers.Handlers;

public class ReceiveMoneyHandler(ITransferDbContext context, ICustomerService customerService) : IRequestHandler<ReceiveMoneyCommand, ServiceResponse<bool>>
{
    public async Task<ServiceResponse<bool>> Handle(ReceiveMoneyCommand request, CancellationToken cancellationToken)
    {
        var transfer = await context.Transfers
            .FirstOrDefaultAsync(x => x.TransactionCode == request.TransactionCode, cancellationToken);

        if (transfer == null)
        {
            throw new BusinessException("Geçersiz işlem kodu.", 404);
        }

        if (transfer.Status == TransactionStatus.CANCELLED || transfer.Status == TransactionStatus.FAILED)
        {
            throw new BusinessException("Bu işlem ödeme için uygun değil.");
        }

        var customerResponse = await customerService.GetCustomerAsync(request.ReceiverCustomerId);
        if (customerResponse?.Data == null || customerResponse.Data.Status == CustomerStatus.Blocked)
        {
            throw new BusinessException("Alıcı kimliği doğrulanamadı veya müşteri engellenmiş.");
        }

        if (transfer.ReceiverCustomerId != request.ReceiverCustomerId)
        {
            throw new BusinessException("Bu işlem kodu bu alıcıya ait değil.");
        }

        transfer.Status = TransactionStatus.COMPLETED;
        transfer.UpdatedDate = DateTime.UtcNow;
        transfer.UpdatedBy = "Branch_User_01";

        await context.SaveChangesAsync(cancellationToken);

        return ServiceResponse<bool>.Success(true);
    }
}