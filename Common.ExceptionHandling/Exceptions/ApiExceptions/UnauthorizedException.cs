namespace Common.ExceptionHandling.Exceptions.ApiExceptions;

public class UnauthorizedException(string message) : ApiException(401, message)
{
}