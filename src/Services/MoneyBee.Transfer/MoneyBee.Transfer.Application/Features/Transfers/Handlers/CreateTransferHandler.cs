using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyBee.Shared.Enums;
using MoneyBee.Transfer.Application.Features.Transfers.Commands;
using MoneyBee.Transfer.Application.Interfaces;
using MoneyBee.Transfer.Application.Interfaces.Persistance;

namespace MoneyBee.Transfer.Application.Features.Transfers.Handlers;

public class CreateTransferHandler(
    ITransferDbContext _context,
    IFraudService _fraudService,
    IExchangeRateService _exchangeRateService,
    ICustomerService _customerService) : IRequestHandler<CreateTransferCommand, Guid>
{
    public async Task<Guid> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var sender = await _customerService.GetCustomerAsync(request.SenderCustomerId);
        var receiver = await _customerService.GetCustomerAsync(request.ReceiverCustomerId);

        if (sender == null || receiver == null)
            throw new Exception("Gönderen veya alıcı müşteri bulunamadı.");

        if (sender.Status == CustomerStatus.Blocked)
            throw new Exception("Engellenmiş müşteriler transfer yapamaz.");

        decimal amountInTry = await _exchangeRateService.ConvertToTryAsync(request.Amount, request.Currency);

        var startOfDay = DateTime.UtcNow.Date;
        var totalSentToday = await _context.Transfers
            .Where(t => t.SenderCustomerId == request.SenderCustomerId && 
                        t.CreatedDate >= startOfDay && 
                        t.Status != TransactionStatus.FAILED)
            .SumAsync(t => t.AmountInTry, cancellationToken);

        if (totalSentToday + amountInTry > 10000)
            throw new Exception("Günlük transfer limiti (10.000 TRY) aşıldı.");

        var riskScore = await _fraudService.CheckRiskAsync(request.SenderCustomerId, amountInTry, "TRY");
        
        if (riskScore == "HIGH")
            throw new Exception("İşlem yüksek risk nedeniyle reddedildi.");

        var status = TransactionStatus.COMPLETED;
        if (amountInTry > 1000)
            status = TransactionStatus.PENDING;

        var transfer = new Domain.Entities.Transfer
        {
            SenderCustomerId = request.SenderCustomerId,
            ReceiverCustomerId = request.ReceiverCustomerId,
            Amount = request.Amount,
            Currency = request.Currency,
            AmountInTry = amountInTry,
            Fee = CalculateFee(amountInTry),
            TransactionCode = GenerateTransactionCode(), 
            Status = status,
            CreatedBy = "Branch_User_01"
        };

        _context.Transfers.Add(transfer);
        await _context.SaveChangesAsync(cancellationToken);

        return transfer.Id;
    }

    private string GenerateTransactionCode() => 
        "MB" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

    private decimal CalculateFee(decimal amountTry) => 
        amountTry * 0.02m;
}