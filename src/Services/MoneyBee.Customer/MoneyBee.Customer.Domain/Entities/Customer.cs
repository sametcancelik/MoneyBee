
using MoneyBee.Shared.Entities;
using MoneyBee.Shared.Enums;

namespace MoneyBee.Customer.Domain.Entities;

public class Customer : AuditableEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string NationalId { get; set; } = null!; 
    public string PhoneNumber { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public CustomerType Type { get; set; }
    public string? TaxNumber { get; set; }
    public bool IsKycVerified { get; set; }
    public CustomerStatus Status { get; set; } = CustomerStatus.Active;
}