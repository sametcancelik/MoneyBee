using System;
using MoneyBee.Shared.Entities;

namespace MoneyBee.Customer.Domain.Entities;

public class Account : AuditableEntity
{
	public Guid CustomerId { get; set; }

	public string AccountNumber { get; set; }

	public decimal Balance { get; set; }

	public string Currency { get; set; } = "TRY";

	public bool IsActive { get; set; } = true;
}
