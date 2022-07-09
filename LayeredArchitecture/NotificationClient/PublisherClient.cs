using System.Text;
using Azure.Messaging.ServiceBus;
using NotificationClient.Interfaces;

namespace NotificationClient;

internal class PublisherClient: IPublisherClient
{
    private readonly ServiceBusSender _clientSender;

    public PublisherClient(ServiceBusSender clientSender)
    {
        _clientSender = clientSender;
    }

    public Task Publish(string payload)
    {
        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(payload))
        {
            ContentType = "application/json"
        };
        return _clientSender.SendMessageAsync(message);
    }
}