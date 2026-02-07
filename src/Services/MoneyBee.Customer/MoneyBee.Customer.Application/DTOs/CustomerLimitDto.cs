using System;

namespace MoneyBee.Customer.Application.DTOs;

public class CustomerLimitDto
{
	public Guid CustomerId { get; set; }

	public decimal DailyTotalAmount { get; set; }

	public DateTime LastTransactionDate { get; set; }
}
