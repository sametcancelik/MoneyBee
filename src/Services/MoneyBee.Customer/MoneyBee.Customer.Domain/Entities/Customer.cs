using MoneyBee.Shared.Entities;
using MoneyBee.Shared.Enums;

namespace MoneyBee.Customer.Domain.Entities;

public class Customer : AuditableEntity
{
	public string FirstName { get; set; }

	public string LastName { get; set; }

	public string NationalId { get; set; }

	public string PhoneNumber { get; set; }

	public string Email { get; set; }

	public DateTime BirthDate { get; set; }

	public CustomerType Type { get; set; }

	public string? TaxNumber { get; set; }

	public bool IsKycVerified { get; set; }

	public CustomerStatus Status { get; set; } = CustomerStatus.Active;

	public List<Account> Accounts { get; set; }

	public CustomerLimit CustomerLimit { get; set; }
}
