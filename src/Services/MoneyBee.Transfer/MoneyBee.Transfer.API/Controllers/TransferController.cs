using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MoneyBee.Shared;
using MoneyBee.Transfer.Application.Features.Transfers.Commands;

namespace MoneyBee.Transfer.API.Controllers;

[EnableRateLimiting("ApiRateLimit")]
public class TransferController(IMediator mediator) : BaseController
{
    [HttpPost("send")]
    public async Task<IActionResult> SendMoney([FromBody] CreateTransferCommand command)
    {
        return ActionResultInstance(await mediator.Send(command));
    }

    [HttpPost("receive")]
    public async Task<IActionResult> ReceiveMoney([FromBody] ReceiveMoneyCommand command)
    {
        return ActionResultInstance(await mediator.Send(command));
    }

    [HttpPost("cancel")]
    public async Task<IActionResult> CancelTransfer([FromBody] CancelTransferCommand command)
    {
        return ActionResultInstance(await mediator.Send(command));
    }
}