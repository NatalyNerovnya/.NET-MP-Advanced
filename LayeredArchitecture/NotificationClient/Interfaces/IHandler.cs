namespace NotificationClient.Interfaces;

public interface IHandler
{
    Task Handle(string message);
}