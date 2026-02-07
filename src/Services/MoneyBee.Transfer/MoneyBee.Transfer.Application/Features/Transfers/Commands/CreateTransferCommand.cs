using MediatR;
using MoneyBee.Shared.Models;

namespace MoneyBee.Transfer.Application.Features.Transfers.Commands;

public record CreateTransferCommand : IRequest<ServiceResponse<Guid>>, IBaseRequest
{
	public Guid SenderCustomerId { get; init; }

	public Guid ReceiverCustomerId { get; init; }

	public decimal Amount { get; init; }

	public string Currency { get; init; }
}
