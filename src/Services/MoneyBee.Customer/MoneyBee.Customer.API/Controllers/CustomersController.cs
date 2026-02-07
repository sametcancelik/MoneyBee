using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyBee.Customer.Application.Features.Customers.Commands;
using MoneyBee.Customer.Application.Features.Customers.Queries;
using MoneyBee.Shared;

namespace MoneyBee.Customer.API.Controller;

public class CustomerController(IMediator _mediator) : BaseController
{
	[HttpPost]
	public async Task<IActionResult> Create(CreateCustomerCommand command)
	{
		return ActionResultInstance(await _mediator.Send(command));
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(Guid id)
	{
		GetCustomerByIdQuery request = new GetCustomerByIdQuery(id);
		return ActionResultInstance(await _mediator.Send(request));
	}

	[HttpPut("{id}/limit")]
	public async Task<IActionResult> UpdateLimit(Guid id, [FromBody] UpdateLimitRequestCommand request)
	{
		return ActionResultInstance(await _mediator.Send(new UpdateLimitRequestCommand(request.Amount)
		{
			CustomerId = id
		}));
	}
}
