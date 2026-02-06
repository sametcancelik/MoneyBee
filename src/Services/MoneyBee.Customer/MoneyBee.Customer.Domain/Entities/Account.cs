using MoneyBee.Shared.Entities;
using MoneyBee.Shared.Enums;

namespace MoneyBee.Customer.Domain.Entities;
public class Account : AuditableEntity
{
    public Guid CustomerId { get; set; }
    public string AccountNumber { get; set; } = null!;
    public decimal Balance { get; set; } = 0;
    public CurrencyType Currency { get; set; } = CurrencyType.TRY; 
    public bool IsActive { get; set; } = true;
}