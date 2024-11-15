namespace Common.ExceptionHandling.Exceptions.ApiExceptions;

public abstract class ApiException : Exception
{
    public ApiException(string message) : base(message)
    {
    }

    public ApiException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
    
    public int StatusCode { get; init; } = 400;
}