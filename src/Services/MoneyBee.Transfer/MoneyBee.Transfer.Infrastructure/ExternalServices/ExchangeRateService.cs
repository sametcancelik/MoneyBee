using MoneyBee.Shared.Enums;
using MoneyBee.Transfer.Application.Interfaces;
using System.Net.Http.Json;

namespace MoneyBee.Transfer.Infrastructure.ExternalServices;
public class ExchangeRateService(HttpClient _httpClient) : IExchangeRateService
{
    public async Task<decimal> GetRateAsync(CurrencyType fromCurrency, CurrencyType toCurrency)
    {
        if (fromCurrency == toCurrency) return 1;

        try
        {
            var response = await _httpClient.GetAsync($"api/v1/rates?from={fromCurrency}&to={toCurrency}");
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RateResponse>();
                return result?.Rate ?? 1;
            }
            
            throw new Exception("Döviz kuru servisine ulaşılamıyor.");
        }
        catch (Exception)
        {
            throw; 
        }
    }

    public async Task<decimal> ConvertToTryAsync(decimal amount, CurrencyType fromCurrency)
    {
        if (fromCurrency == CurrencyType.TRY) return amount;

        decimal rate = await GetRateAsync(fromCurrency, CurrencyType.TRY);
        return amount * rate;
    }
}

public record RateResponse(decimal Rate);