using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyBee.Customer.Application.Features.Customers.Commands;
using MoneyBee.Customer.Application.Interfaces.Persistance;
using MoneyBee.Shared.Models;

namespace MoneyBee.Customer.Application.Features.Customers.Handlers;

public class UpdateCustomerLimitHandler(ICustomerDbContext context) : IRequestHandler<UpdateLimitRequestCommand, ServiceResponse<bool>>
{
    public async Task<ServiceResponse<bool>> Handle(UpdateLimitRequestCommand request, CancellationToken cancellationToken)
    {
        var customerLimit = await context.CustomerLimits
            .FirstOrDefaultAsync(l => l.CustomerId == request.CustomerId, cancellationToken);

        if (customerLimit == null)
        {
            return ServiceResponse<bool>.Failure("Limit kaydı bulunamadı.", 404);
        }

        var today = DateTime.UtcNow.Date;

        if (customerLimit.LastTransactionDate.Date < today)
        {
            customerLimit.DailyTotalAmount = request.Amount;
        }
        else
        {
            customerLimit.DailyTotalAmount += request.Amount;
        }

        customerLimit.LastTransactionDate = DateTime.UtcNow;
        customerLimit.UpdatedDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return ServiceResponse<bool>.Success(true, "Limit başarıyla güncellendi.");
    }
}