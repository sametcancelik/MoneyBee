using MoneyBee.Shared.Enums;

namespace MoneyBee.Transfer.Application.Interfaces;

public interface IExchangeRateService
{
    Task<decimal> GetRateAsync(CurrencyType fromCurrency, CurrencyType toCurrency);
    Task<decimal> ConvertToTryAsync(decimal amount, CurrencyType fromCurrency);
}
