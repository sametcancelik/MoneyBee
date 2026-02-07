using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MoneyBee.Shared.Models;

public class ServiceResponse
{
	public bool IsSuccess { get; set; }

	public string? Message { get; set; }

	public List<string>? Errors { get; set; }

	[JsonIgnore]
	public int StatusCode { get; set; }

	public static ServiceResponse Success(string message = "İşlem başarılı.", int statusCode = 200)
	{
		return new ServiceResponse
		{
			IsSuccess = true,
			Message = message,
			StatusCode = statusCode
		};
	}

	public static ServiceResponse Failure(string error, int statusCode = 400)
	{
		return new ServiceResponse
		{
			IsSuccess = false,
			Errors = new List<string> { error },
			StatusCode = statusCode
		};
	}

	public static ServiceResponse Failure(List<string> errors, int statusCode = 400)
	{
		return new ServiceResponse
		{
			IsSuccess = false,
			Errors = errors,
			StatusCode = statusCode
		};
	}
}
public class ServiceResponse<T> : ServiceResponse
{
	public T? Data { get; set; }

	public static ServiceResponse<T> Success(T data, string message = "İşlem başarılı.", int statusCode = 200)
	{
		return new ServiceResponse<T>
		{
			Data = data,
			IsSuccess = true,
			Message = message,
			StatusCode = statusCode
		};
	}

	public new static ServiceResponse<T> Failure(string error, int statusCode = 400)
	{
		return new ServiceResponse<T>
		{
			IsSuccess = false,
			Errors = new List<string> { error },
			StatusCode = statusCode
		};
	}
}
