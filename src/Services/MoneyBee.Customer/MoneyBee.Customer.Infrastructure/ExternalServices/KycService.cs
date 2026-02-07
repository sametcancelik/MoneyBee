using System.Globalization;
using System.Net.Http.Json;
using MoneyBee.Customer.Application.Interfaces;

namespace MoneyBee.Customer.Infrastructure.ExternalServices;

public class KycService(HttpClient httpClient) : IKycService
{
    public bool ValidateNationalId(string nationalId)
    {
        if (string.IsNullOrWhiteSpace(nationalId) || nationalId.Length != 11 || nationalId[0] == '0' || !nationalId.All(char.IsDigit))
            return false;

        var digits = nationalId.Select(c => c - '0').ToArray();
        
        int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
        int evenSum = digits[1] + digits[3] + digits[5] + digits[7];

        if ((oddSum * 7 - evenSum) % 10 != digits[9]) return false;
        if (digits.Take(10).Sum() % 10 != digits[10]) return false;

        return true;
    }

    public async Task<bool> VerifyWithExternalServiceAsync(Guid userId, string nationalId, string firstName, string lastName, int birthYear)
    {
        if (!ValidateNationalId(nationalId)) return false;

        try
        {
            var requestBody = new
            {
                userId = userId,
                tcno = nationalId,
                name = firstName,
                surname = lastName,
                birthYear = birthYear
            };

            var response = await httpClient.PostAsJsonAsync("api/kyc/verify", requestBody);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<KycResponse>();
                return result != null && result.Verified;
            }

            return false;
        }
        catch (Exception ex)
        {
            throw new Exception($"KYC doğrulama servisi çağrılırken hata oluştu: {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}

public record KycResponse(bool Success, bool Verified, string Reason);