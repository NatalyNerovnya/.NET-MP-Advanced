using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using NotificationClient.Interfaces;

namespace NotificationClient;

public static class Setup
{
    public static void AddPublisher(this IServiceCollection services)
    {
        var connectionString = "Endpoint=sb://net-mentoring.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=O1O323CxZpkhx7SYesuje0N7UG5onEaHeyUcgbVOJpY=";
        var queueName = "item-changed";
        var client = new ServiceBusClient(connectionString);

        services.AddScoped<ServiceBusSender>(s => client.CreateSender(queueName));
        services.AddScoped<IPublisherClient, PublisherClient>();
    }

    public static void AddListener(this IServiceCollection services)
    {
        var connectionString = "Endpoint=sb://net-mentoring.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=O1O323CxZpkhx7SYesuje0N7UG5onEaHeyUcgbVOJpY=";
        var queueName = "item-changed";
        var client = new ServiceBusClient(connectionString);

        services.AddSingleton<ServiceBusReceiver>(s => client.CreateReceiver(queueName));

        services.AddHostedService<ListenerClient>();
    }
}