using MoneyBee.Customer.Application.DTOs;

namespace MoneyBee.Transfer.Application.Interfaces;

public interface ICustomerService
{
    Task<CustomerDto?> GetCustomerAsync(Guid customerId);
}
