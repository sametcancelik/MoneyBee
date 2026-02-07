using System.Net.Http.Json;
using MoneyBee.Shared.Exceptions;
using MoneyBee.Transfer.Application.Interfaces;

namespace MoneyBee.Transfer.Infrastructure.ExternalServices;

public class ExchangeRateService(HttpClient httpClient) : IExchangeRateService
{
    public async Task<decimal> GetRateAsync(string fromCurrency, string toCurrency)
    {
        if (fromCurrency == toCurrency)
            return 1m;

        try
        {
            var response = await httpClient.GetAsync($"api/exchange/convert?from={fromCurrency}&to={toCurrency}&amount=100");
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RateResponse>();
                if (result != null && result.Rate > 0m)
                {
                    return result.Rate;
                }
            }
            throw new BusinessException("Döviz kuru servisi geçersiz yanıt döndü.");
        }
        catch (Exception ex) when (ex is not BusinessException)
        {
            return (fromCurrency, toCurrency) switch
            {
                ("USD", "TRY") => 33.5m,
                ("EUR", "TRY") => 36.0m,
                _ => throw new BusinessException("Döviz kuru bilgisi alınamıyor.")
            };
        }
    }

    public async Task<decimal> ConvertToTryAsync(decimal amount, string fromCurrency)
    {
        if (fromCurrency == "TRY")
            return amount;

        var rate = await GetRateAsync(fromCurrency, "TRY");
        return Math.Round(amount * rate, 2);
    }
}

public record RateResponse(string From, string To, decimal Amount, decimal Converted, decimal Rate, long Timestamp);