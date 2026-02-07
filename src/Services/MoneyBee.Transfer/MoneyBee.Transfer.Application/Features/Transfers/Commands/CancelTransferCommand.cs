using MediatR;
using MoneyBee.Shared.Models;

namespace MoneyBee.Transfer.Application.Features.Transfers.Commands;
public record CancelTransferCommand : IRequest<ServiceResponse<bool>>, IBaseRequest
{
	public string TransactionCode { get; init; }
}
