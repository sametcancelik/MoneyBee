using System;

namespace MoneyBee.Customer.Application.DTOs;

public record AccountDto
{
	public Guid Id { get; init; }

	public string AccountNumber { get; init; }

	public decimal Balance { get; init; }

	public string Currency { get; init; }
}
