using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyBee.Customer.Application.DTOs;
using MoneyBee.Customer.Application.Features.Customers.Queries;
using MoneyBee.Customer.Application.Interfaces.Persistance;
using MoneyBee.Shared.Exceptions;
using MoneyBee.Shared.Models;

namespace MoneyBee.Customer.Application.Features.Customers.Handlers;

public class GetCustomerByIdQueryHandler(ICustomerDbContext context) : IRequestHandler<GetCustomerByIdQuery, ServiceResponse<CustomerDto?>>
{
    public async Task<ServiceResponse<CustomerDto?>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await context.Customers
            .Include(c => c.Accounts)
            .Include(c => c.CustomerLimit)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (customer == null)
        {
            throw new BusinessException("Müşteri bulunamadı.", 404);
        }

        var customerDto = new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Accounts = customer.Accounts.Select(a => new AccountDto
            {
                Id = a.Id,
                AccountNumber = a.AccountNumber,
                Balance = a.Balance
            }).ToList(),
            CustomerLimit = customer.CustomerLimit != null ? new CustomerLimitDto
            {
                CustomerId = customer.Id,
                DailyTotalAmount = customer.CustomerLimit.DailyTotalAmount,
                LastTransactionDate = customer.CustomerLimit.LastTransactionDate
            } : null
        };

        return ServiceResponse<CustomerDto?>.Success(customerDto);
    }
}