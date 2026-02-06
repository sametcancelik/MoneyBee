using MoneyBee.Customer.Application.Interfaces;
using System.Net.Http.Json;

namespace MoneyBee.Customer.Infrastructure.ExternalServices;

public class KycService(HttpClient _httpClient) : IKycService
{
    public bool ValidateNationalId(string nationalId)
    {
        if (string.IsNullOrWhiteSpace(nationalId) || nationalId.Length != 11 || nationalId[0] == '0' || !nationalId.All(char.IsDigit))
            return false;

        int[] digits = nationalId.Select(c => int.Parse(c.ToString())).ToArray();

        int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
        int evenSum = digits[1] + digits[3] + digits[5] + digits[7];

        if ((oddSum * 7 - evenSum) % 10 != digits[9])
            return false;

        if (digits.Take(10).Sum() % 10 != digits[10])
            return false;

        return true;
    }

    public async Task<bool> VerifyWithExternalServiceAsync(string nationalId, string firstName, string lastName, int birthYear)
    {
        try
        {
            var requestBody = new 
            { 
                TcNo = nationalId, 
                Ad = firstName.ToUpper(), 
                Soyad = lastName.ToUpper(), 
                DogumYili = birthYear 
            };

            var response = await _httpClient.PostAsJsonAsync("api/v1/validate-identity", requestBody);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
}