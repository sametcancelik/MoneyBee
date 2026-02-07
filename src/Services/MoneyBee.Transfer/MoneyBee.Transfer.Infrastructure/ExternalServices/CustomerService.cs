using System.Net.Http.Json;
using MoneyBee.Shared.Exceptions;
using MoneyBee.Shared.Models;
using MoneyBee.Transfer.Application.DTOs;
using MoneyBee.Transfer.Application.Interfaces;

namespace MoneyBee.Transfer.Infrastructure.ExternalServices;

public class CustomerService(HttpClient httpClient) : ICustomerService
{
    public async Task<ServiceResponse<CustomerDto>> GetCustomerAsync(Guid customerId)
    {
        try
        {
            var response = await httpClient.GetAsync($"api/Customer/{customerId}");

            if (response.IsSuccessStatusCode)
            {
                var serviceResponse = await response.Content.ReadFromJsonAsync<ServiceResponse<CustomerDto>>();
                if (serviceResponse is { IsSuccess: true })
                {
                    return ServiceResponse<CustomerDto>.Success(serviceResponse.Data, serviceResponse.Message, serviceResponse.StatusCode);
                }
            }

            throw new BusinessException($"Müşteri bilgisi alınamadı. İstek: {response.RequestMessage?.RequestUri}");
        }
        catch (BusinessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Müşteri servisine erişilemedi: {ex.Message}", 503);
        }
    }

    public async Task<ServiceResponse> UpdateCustomerLimitAsync(Guid senderCustomerId, decimal amountInTry)
    {
        var response = await httpClient.PutAsJsonAsync($"api/Customer/{senderCustomerId}/limit", new
        {
            Amount = amountInTry
        });

        if (!response.IsSuccessStatusCode)
        {
            throw new BusinessException("Limit güncellenemedi.");
        }

        return ServiceResponse.Success("Limit başarıyla güncellendi.");
    }
}