using MediatR;

namespace MoneyBee.Transfer.Application.Features.Transfers.Commands;

public record ReceiveMoneyCommand : IRequest<bool>
{
    public string TransactionCode { get; init; } 
    public Guid ReceiverCustomerId { get; init; }
}
