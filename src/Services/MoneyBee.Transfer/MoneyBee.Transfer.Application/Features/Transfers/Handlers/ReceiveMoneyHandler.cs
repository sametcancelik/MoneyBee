using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyBee.Shared.Enums;
using MoneyBee.Transfer.Application.Interfaces;
using MoneyBee.Transfer.Application.Interfaces.Persistance;

namespace MoneyBee.Transfer.Application.Features.Transfers.Commands;
public class ReceiveMoneyHandler(
    ITransferDbContext _context,
    ICustomerService _customerService) : IRequestHandler<ReceiveMoneyCommand, bool>
{
    public async Task<bool> Handle(ReceiveMoneyCommand request, CancellationToken cancellationToken)
    {
        var transfer = await _context.Transfers
            .FirstOrDefaultAsync(x => x.TransactionCode == request.TransactionCode, cancellationToken);

        if (transfer == null)
            throw new Exception("Geçersiz işlem kodu.");

        if (transfer.Status == TransactionStatus.CANCELLED || transfer.Status == TransactionStatus.FAILED)
            throw new Exception("Bu işlem ödeme için uygun değil.");

        var customer = await _customerService.GetCustomerAsync(request.ReceiverCustomerId);
        if (customer == null || customer.Status == CustomerStatus.Blocked)
            throw new Exception("Alıcı kimliği doğrulanamadı veya müşteri engellenmiş.");

        if (transfer.ReceiverCustomerId != request.ReceiverCustomerId)
            throw new Exception("Bu işlem kodu bu alıcıya ait değil.");

        transfer.Status = TransactionStatus.COMPLETED;
        transfer.UpdatedDate = DateTime.UtcNow;
        transfer.UpdatedBy = "Branch_User_01"; 

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}