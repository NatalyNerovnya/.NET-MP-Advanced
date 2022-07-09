namespace NotificationClient.Interfaces;

public interface IPublisherClient
{
    Task Publish(string payload);
}