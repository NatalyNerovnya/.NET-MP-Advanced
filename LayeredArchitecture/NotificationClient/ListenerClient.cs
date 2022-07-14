using System.Text;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using NotificationClient.Interfaces;

namespace NotificationClient;

public class ListenerClient : BackgroundService
{
    private readonly ServiceBusReceiver _clientReceiver;
    private readonly IHandler _handler;

    public ListenerClient(ServiceBusReceiver clientReceiver, IHandler handler)
    {
        _clientReceiver = clientReceiver;
        _handler = handler;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Listener service has been started.");

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var message = await _clientReceiver.ReceiveMessageAsync(cancellationToken: cancellationToken);
                await _handler.Handle(Encoding.UTF8.GetString(message.Body));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception during message processing");
            }
        }

    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Listener service has been stopped.");
        return _clientReceiver.CloseAsync(cancellationToken);
    }
}