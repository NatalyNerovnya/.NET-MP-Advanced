using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using NotificationClient.Interfaces;

namespace NotificationClient;

public static class Setup
{
    public static void AddNotification(this IServiceCollection services)
    {
        var connectionString = "";
        var queueName = "item-changed";

        services.AddScoped<ServiceBusSender>(s =>
        {
            var client = new ServiceBusClient(connectionString);
            return client.CreateSender(queueName);
        });

        services.AddScoped<IListenerClient, ListenerClient>();
        services.AddScoped<IPublisherClient, PublisherClient>();
    }
}