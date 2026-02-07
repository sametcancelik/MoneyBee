using Microsoft.AspNetCore.Mvc;
using MoneyBee.Shared.Models;

namespace MoneyBee.Shared;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
	[NonAction]
	public ObjectResult ActionResultInstance<T>(ServiceResponse<T> response)
	{
		return new ObjectResult(response)
		{
			StatusCode = response.StatusCode
		};
	}

	[NonAction]
	public ObjectResult ActionResultInstance(ServiceResponse response)
	{
		return new ObjectResult(response)
		{
			StatusCode = response.StatusCode
		};
	}
}
