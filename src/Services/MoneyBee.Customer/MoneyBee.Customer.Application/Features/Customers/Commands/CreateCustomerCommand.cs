using System;
using MediatR;
using MoneyBee.Shared.Enums;
using MoneyBee.Shared.Models;

namespace MoneyBee.Customer.Application.Features.Customers.Commands;

public record CreateCustomerCommand : IRequest<ServiceResponse<Guid>>, IBaseRequest
{
	public string FirstName { get; init; }

	public string LastName { get; init; }

	public string NationalId { get; init; }

	public DateTime BirthDate { get; init; }

	public string PhoneNumber { get; init; }

	public string Email { get; init; }

	public string? TaxNumber { get; set; }

	public CustomerType Type { get; init; }

	public string Currency { get; init; }
}
