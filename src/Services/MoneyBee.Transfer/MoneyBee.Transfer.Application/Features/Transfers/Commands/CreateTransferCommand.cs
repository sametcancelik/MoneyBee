using MediatR;
using MoneyBee.Shared.Enums;

namespace MoneyBee.Transfer.Application.Features.Transfers.Commands;

public record CreateTransferCommand : IRequest<Guid>
{
    public Guid SenderCustomerId { get; init; }
    public Guid ReceiverCustomerId { get; init; }
    public decimal Amount { get; init; }
    public CurrencyType Currency { get; init; }
}