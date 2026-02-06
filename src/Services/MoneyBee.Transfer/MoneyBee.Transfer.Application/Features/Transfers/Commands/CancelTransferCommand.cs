using MediatR;

namespace MoneyBee.Transfer.Application.Features.Transfers.Commands;

public record CancelTransferCommand : IRequest<bool>
{
    public string TransactionCode { get; init; }
}
