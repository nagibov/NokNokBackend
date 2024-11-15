namespace Common.ExceptionHandling.Exceptions.ApiExceptions;

public class NotFoundException(string message) : ApiException(404, message)
{
}