namespace MoneyBee.Transfer.Application.Interfaces;

public interface IExchangeRateService
{
    Task<decimal> GetRateAsync(string fromCurrency, string toCurrency);

    Task<decimal> ConvertToTryAsync(decimal amount, string fromCurrency);
}