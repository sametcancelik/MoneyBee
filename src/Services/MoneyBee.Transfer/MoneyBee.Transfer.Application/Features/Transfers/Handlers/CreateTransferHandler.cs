using MediatR;
using MoneyBee.Shared.Enums;
using MoneyBee.Shared.Exceptions;
using MoneyBee.Shared.Models;
using MoneyBee.Transfer.Application.DTOs;
using MoneyBee.Transfer.Application.Features.Transfers.Commands;
using MoneyBee.Transfer.Application.Interfaces;
using MoneyBee.Transfer.Application.Interfaces.Persistance;
using MoneyBee.Transfer.Domain.Entities;

namespace MoneyBee.Transfer.Application.Features.Transfers.Handlers;

public class CreateTransferHandler(
    ITransferDbContext context, 
    IFraudService fraudService, 
    IExchangeRateService exchangeRateService, 
    ICustomerService customerService) : IRequestHandler<CreateTransferCommand, ServiceResponse<Guid>>
{
    public async Task<ServiceResponse<Guid>> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var customerResponse = await customerService.GetCustomerAsync(request.SenderCustomerId);
        if (!customerResponse.IsSuccess)
        {
            throw new BusinessException("Gönderen müşteri bulunamadı.", 404);
        }

        var sender = customerResponse.Data;
        if (sender.Status == CustomerStatus.Blocked)
        {
            throw new BusinessException("Engellenmiş müşteriler transfer yapamaz.", 403);
        }

        decimal amountInTry = await exchangeRateService.ConvertToTryAsync(request.Amount, request.Currency);
        
        var dailyTotal = sender.CustomerLimit.LastTransactionDate.Date == DateTime.UtcNow.Date 
            ? sender.CustomerLimit.DailyTotalAmount 
            : 0m;

        if (dailyTotal + amountInTry > 10000m)
        {
            throw new BusinessException("Günlük transfer limiti (10000 TL) aşıldı.");
        }

        var riskLevel = await fraudService.CheckRiskAsync(request.SenderCustomerId, amountInTry, request.Currency);
        if (riskLevel == "HIGH")
        {
            throw new BusinessException("İşlem yüksek risk nedeniyle reddedildi.");
        }

        var status = (riskLevel == "MEDIUM" || amountInTry > 1000m) 
            ? TransactionStatus.PENDING 
            : TransactionStatus.COMPLETED;

        var transfer = new Domain.Entities.Transfer
        {
            SenderCustomerId = request.SenderCustomerId,
            ReceiverCustomerId = request.ReceiverCustomerId,
            Amount = request.Amount,
            Currency = request.Currency,
            AmountInTry = amountInTry,
            Fee = CalculateFee(amountInTry),
            TransactionCode = $"MB{Guid.NewGuid():N}"[..10].ToUpper(),
            Status = status,
            CreatedBy = "Transfer_Service",
            CreatedDate = DateTime.UtcNow
        };

        context.Transfers.Add(transfer);
        await customerService.UpdateCustomerLimitAsync(request.SenderCustomerId, amountInTry);
        await context.SaveChangesAsync(cancellationToken);

        return ServiceResponse<Guid>.Success(transfer.Id, "İşlem başarıyla kaydedildi.", 201);
    }

    private static decimal CalculateFee(decimal amountTry) => Math.Round(amountTry * 0.02m, 2);
}