using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MoneyBee.Transfer.Application.Features.Transfers.Commands;

namespace MoneyBee.Transfer.API.Controllers;

[ApiController]
[Route("api/v1/transfers")]
[EnableRateLimiting("ApiRateLimit")] // 
public class TransferController(IMediator _mediator) : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> SendMoney([FromBody] CreateTransferCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { TransferId = result, Message = "Transfer başarıyla oluşturuldu." });
    }

    [HttpPost("receive")]
    public async Task<IActionResult> ReceiveMoney([FromBody] ReceiveMoneyCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { Success = result, Message = "Ödeme başarıyla tamamlandı." });
    }

    [HttpPost("cancel")]
    public async Task<IActionResult> CancelTransfer([FromBody] CancelTransferCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { Success = result, Message = "İşlem iptal edildi ve ücret iade edildi." });
    }
}