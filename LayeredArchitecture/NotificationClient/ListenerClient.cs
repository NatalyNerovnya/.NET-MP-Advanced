using System.Text;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using NotificationClient.Interfaces;

namespace NotificationClient;

public class ListenerClient: IHostedService
{
    private readonly ServiceBusReceiver _clientReceiver;
    private readonly IHandler _handler;

    public ListenerClient(ServiceBusReceiver clientReceiver, IHandler handler)
    {
        _clientReceiver = clientReceiver;
        _handler = handler;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var message = await _clientReceiver.ReceiveMessageAsync(cancellationToken: cancellationToken);

        await _handler.Handle(Encoding.UTF8.GetString(message.Body));

        Console.WriteLine("Listener service has been started.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Listener service has been stopped.");
        return _clientReceiver.CloseAsync(cancellationToken);
    }
}