namespace MoneyBee.Transfer.Application.Interfaces;

public interface IFraudService
{
	Task<string> CheckRiskAsync(Guid customerId, decimal amount, string currency);
}
