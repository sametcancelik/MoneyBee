using System;
using MediatR;
using MoneyBee.Shared.Models;

namespace MoneyBee.Customer.Application.Features.Customers.Commands;

public record UpdateLimitRequestCommand(decimal Amount) : IRequest<ServiceResponse<bool>>, IBaseRequest
{
	public Guid CustomerId { get; set; }
}
