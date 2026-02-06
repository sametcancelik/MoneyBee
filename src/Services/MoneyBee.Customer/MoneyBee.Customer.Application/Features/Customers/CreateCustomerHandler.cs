using MediatR;
using MoneyBee.Customer.Application.Features.Customers.Commands;
using MoneyBee.Customer.Application.Interfaces.Persistance;
using MoneyBee.Customer.Domain.Entities;

namespace MoneyBee.Customer.Application.Features.Customers;

public class CreateCustomerHandler(ICustomerDbContext _context) : IRequestHandler<CreateCustomerCommand, Guid>
{
    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Domain.Entities.Customer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            NationalId = request.NationalId,
            BirthDate = request.BirthDate,
            PhoneNumber = request.PhoneNumber,
            Type = request.Type,
            IsKycVerified = false 
        };

        _context.Customers.Add(customer);

        var account = new Account
        {
            CustomerId = customer.Id,
            AccountNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16),
            Balance = 0,
            Currency = "TRY"
        };
        
        _context.Accounts.Add(account);

        var limit = new CustomerLimit
        {
            CustomerId = customer.Id,
            DailyTotalAmount = 0,
            LastTransactionDate = DateTime.UtcNow
        };

        _context.CustomerLimits.Add(limit);

        await _context.SaveChangesAsync(cancellationToken);
        return customer.Id;
    }
}