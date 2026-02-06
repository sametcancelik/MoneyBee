using MoneyBee.Customer.Application.DTOs;
using MoneyBee.Transfer.Application.Interfaces;
using System.Net.Http.Json;

namespace MoneyBee.Transfer.Infrastructure.ExternalServices;
public class CustomerService(HttpClient _httpClient) : ICustomerService
{
    public async Task<CustomerDto?> GetCustomerAsync(Guid customerId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/v1/customers/{customerId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CustomerDto>();
            }

            return null;
        }
        catch (Exception)
        {
            return null; 
        }
    }
}