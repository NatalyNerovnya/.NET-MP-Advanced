namespace CategoryService.Application.Exceptions;

public class NotExistException : Exception
{
    public NotExistException() : base()
    {
    }

    public NotExistException(string message) : base(message)
    {
    }

    public NotExistException(string message, Exception innerException) : base(message, innerException)
    {
    }

}