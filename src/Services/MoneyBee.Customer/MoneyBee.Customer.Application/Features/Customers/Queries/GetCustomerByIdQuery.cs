using System;
using MediatR;
using MoneyBee.Customer.Application.DTOs;
using MoneyBee.Shared.Models;

namespace MoneyBee.Customer.Application.Features.Customers.Queries;

public record GetCustomerByIdQuery(Guid Id) : IRequest<ServiceResponse<CustomerDto>>, IBaseRequest;
