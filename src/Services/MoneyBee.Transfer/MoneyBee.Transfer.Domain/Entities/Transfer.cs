using MoneyBee.Shared.Entities;
using MoneyBee.Shared.Enums;

namespace MoneyBee.Transfer.Domain.Entities;
public class Transfer : AuditableEntity
{
    public Guid SenderCustomerId { get; set; }
    public Guid ReceiverCustomerId { get; set; }
    public decimal Amount { get; set; }
    public CurrencyType Currency { get; set; } 
    public decimal AmountInTry { get; set; }
    public decimal Fee { get; set; }
    public string TransactionCode { get; set; }
    public TransactionStatus Status { get; set; }
    public DateTime? ApprovedDate { get; set; }
}