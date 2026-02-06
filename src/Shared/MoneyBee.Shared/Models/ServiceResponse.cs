namespace MoneyBee.Shared.Models;

public class ServiceResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ServiceResponse Success(string? message = null) 
        => new() { IsSuccess = true, Message = message };

    public static ServiceResponse Failure(string error) 
        => new() { IsSuccess = false, Errors = new List<string> { error } };
}

public class ServiceResponse<T> : ServiceResponse
{
    public T? Data { get; set; }

    public static ServiceResponse<T> Success(T data, string? message = null) 
        => new() { IsSuccess = true, Data = data, Message = message };

    public new static ServiceResponse<T> Failure(string error) 
        => new() { IsSuccess = false, Errors = new List<string> { error } };
}
