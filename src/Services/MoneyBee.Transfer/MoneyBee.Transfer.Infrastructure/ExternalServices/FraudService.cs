using System.Net.Http.Json;
using MoneyBee.Transfer.Application.Interfaces;

namespace MoneyBee.Transfer.Infrastructure.ExternalServices;

public class FraudService(HttpClient httpClient) : IFraudService
{
    public async Task<string> CheckRiskAsync(Guid customerId, decimal amount, string currency)
    {
        try
        {
            var requestBody = new
            {
                transactionId = $"TXN-{Guid.NewGuid()}",
                userId = customerId.ToString(),
                toUserId = "SYSTEM_RECEIVER",
                amount,
                currency,
                metadata = new
                {
                    description = "Money transfer risk check",
                    category = "transfer",
                    deviceId = "MONEYBEE-APP-01",
                    ipAddress = "127.0.0.1"
                }
            };

            var response = await httpClient.PostAsJsonAsync("api/fraud/check", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<FraudApiResponse>();
                return result?.Data?.RiskLevel ?? "HIGH";
            }

            return "HIGH";
        }
        catch
        {
            return "HIGH";
        }
    }
}

public record FraudApiResponse(bool Success, FraudData Data);

public record FraudData(
    string TransactionId, 
    string RiskLevel, 
    int RiskScore, 
    List<string> RiskFactors, 
    bool ShouldBlock, 
    List<string> Recommendations, 
    List<string> RequiredActions, 
    int ProcessingTime);