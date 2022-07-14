using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using NotificationClient.Interfaces;

namespace NotificationClient;

public static class Setup
{
    public static void AddPublisher(this IServiceCollection services)
    {
        var connectionString = "";
        var queueName = "item-changed";
        var client = new ServiceBusClient(connectionString);

        services.AddScoped<ServiceBusSender>(s => client.CreateSender(queueName));
        services.AddScoped<IPublisherClient, PublisherClient>();
    }

    public static void AddListener(this IServiceCollection services)
    {
        var connectionString = "";
        var queueName = "item-changed";
        var client = new ServiceBusClient(connectionString);

        services.AddHostedService<ListenerClient>();
        services.AddScoped<ServiceBusReceiver>(s => client.CreateReceiver(queueName));
    }
}