using MediatR;
using MoneyBee.Shared.Enums;

namespace MoneyBee.Customer.Application.Features.Customers.Commands;

public record CreateCustomerCommand : IRequest<Guid>
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string NationalId { get; init; } = null!;
    public DateTime BirthDate { get; init; }
    public string PhoneNumber { get; init; } = null!;
    public string? TaxNumber { get; set; }
    public CustomerType Type { get; init; }
}