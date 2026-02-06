using System.Net.Http.Json;
using MoneyBee.Transfer.Application.Interfaces;

namespace MoneyBee.Transfer.Infrastructure.ExternalServices;

public class FraudService(HttpClient _httpClient) : IFraudService
{
    public async Task<string> CheckRiskAsync(Guid customerId, decimal amount, string currency)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/fraud-check", new { customerId, amount, currency });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<FraudResponse>();
                return result?.RiskScore ?? "HIGH";
            }
            return "HIGH";
        }
        catch { return "HIGH"; }
    }
}

public record FraudResponse(string RiskScore);