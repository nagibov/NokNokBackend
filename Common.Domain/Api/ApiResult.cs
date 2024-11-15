namespace Common.Domain.Api;

public record ApiResult<TResponse>
{
    public bool Succeeded { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public TResponse? Data { get; set; }
}