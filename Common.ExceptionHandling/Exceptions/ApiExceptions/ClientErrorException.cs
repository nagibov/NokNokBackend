namespace Common.ExceptionHandling.Exceptions.ApiExceptions;

public class ClientErrorException(string message) : ApiException(400, message)
{
}