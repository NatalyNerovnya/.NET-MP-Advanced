namespace CartingService.Entities.Exceptions;

public class ItemDuplicateException : Exception
{
    public ItemDuplicateException()
    {
    }

    public ItemDuplicateException(string message)
        : base(message)
    {
    }

    public ItemDuplicateException(string message, Exception inner)
        : base(message, inner)
    {
    }
}