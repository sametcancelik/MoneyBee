using MoneyBee.Shared.Entities;

namespace MoneyBee.Customer.Domain.Entities;
public class CustomerLimit : AuditableEntity
{
    public Guid CustomerId { get; set; }
    public decimal DailyTotalAmount { get; set; } 
    public DateTime LastTransactionDate { get; set; }
}