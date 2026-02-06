using MoneyBee.Shared.Enums;

namespace MoneyBee.Customer.Application.DTOs;

public record CustomerDto(
    Guid Id, 
    string FirstName, 
    string LastName, 
    CustomerStatus Status,
    CustomerType Type);
