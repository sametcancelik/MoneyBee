using MoneyBee.Shared.Models;
using MoneyBee.Transfer.Application.DTOs;

namespace MoneyBee.Transfer.Application.Interfaces;

public interface ICustomerService
{
    Task<ServiceResponse<CustomerDto>> GetCustomerAsync(Guid customerId);

    Task<ServiceResponse> UpdateCustomerLimitAsync(Guid senderCustomerId, decimal amount);
}