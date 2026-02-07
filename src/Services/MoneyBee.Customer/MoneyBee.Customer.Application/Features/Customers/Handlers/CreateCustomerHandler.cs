using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyBee.Customer.Application.Features.Customers.Commands;
using MoneyBee.Customer.Application.Interfaces;
using MoneyBee.Customer.Application.Interfaces.Persistance;
using MoneyBee.Customer.Domain.Entities;
using MoneyBee.Shared.Exceptions;
using MoneyBee.Shared.Models;

namespace MoneyBee.Customer.Application.Features.Customers.Handlers;

public class CreateCustomerHandler(ICustomerDbContext context, IKycService kycService) 
    : IRequestHandler<CreateCustomerCommand, ServiceResponse<Guid>>
{
    public async Task<ServiceResponse<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (await context.Customers.AnyAsync(c => c.NationalId == request.NationalId, cancellationToken))
        {
            throw new BusinessException("Bu kimlik numarasıyla zaten bir müşteri mevcut.");
        }

        Guid newCustomerId = Guid.NewGuid();

        var isVerified = await kycService.VerifyWithExternalServiceAsync(
            newCustomerId, 
            request.NationalId, 
            request.FirstName, 
            request.LastName, 
            request.BirthDate.Year);

        if (!isVerified)
        {
            throw new BusinessException("Kimlik bilgileri doğrulanamadı.");
        }

        var customer = new Domain.Entities.Customer
        {
            Id = newCustomerId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            NationalId = request.NationalId,
            BirthDate = request.BirthDate,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Type = request.Type,
            IsKycVerified = true
        };

        var account = new Account
        {
            CustomerId = customer.Id,
            AccountNumber = Guid.NewGuid().ToString("N")[..16].ToUpper(),
            Balance = 0m,
            Currency = request.Currency
        };

        var limit = new CustomerLimit
        {
            CustomerId = customer.Id,
            DailyTotalAmount = 0m,
            LastTransactionDate = DateTime.UtcNow
        };

        context.Customers.Add(customer);
        context.Accounts.Add(account);
        context.CustomerLimits.Add(limit);

        await context.SaveChangesAsync(cancellationToken);

        return ServiceResponse<Guid>.Success(customer.Id);
    }
}