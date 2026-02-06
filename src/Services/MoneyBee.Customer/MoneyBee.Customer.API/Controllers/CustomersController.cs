using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyBee.Customer.Application.Features.Customers.Commands;

namespace MoneyBee.Customer.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
