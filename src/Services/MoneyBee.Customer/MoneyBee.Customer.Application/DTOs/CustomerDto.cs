using System;
using System.Collections.Generic;
using MoneyBee.Shared.Enums;

namespace MoneyBee.Customer.Application.DTOs;

public record CustomerDto
{
	public Guid Id { get; init; }

	public string FirstName { get; init; }

	public string LastName { get; init; }

	public string Email { get; init; }

	public CustomerStatus Status { get; set; }

	public List<AccountDto> Accounts { get; init; } = new List<AccountDto>();

	public CustomerLimitDto CustomerLimit { get; set; } = new CustomerLimitDto();
}
