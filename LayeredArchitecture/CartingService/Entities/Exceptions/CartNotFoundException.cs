namespace CartingService.Entities.Exceptions;

public class CartNotFoundException : Exception
{
    public CartNotFoundException()
    {
    }

    public CartNotFoundException(string message)
        : base(message)
    {
    }

    public CartNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}