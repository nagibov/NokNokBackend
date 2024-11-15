namespace Common.ExceptionHandling.Exceptions.ApiExceptions;

public class ForbiddenAccessException(string message) : ApiException(403, message)
{
}