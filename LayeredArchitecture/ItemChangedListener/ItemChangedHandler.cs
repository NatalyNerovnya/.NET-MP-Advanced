using System.Text.Json;
using CartingService.BLL;
using CartingService.Entities.Models;
using NotificationClient.Interfaces;

namespace ItemChangedListener;

public class ItemChangedHandler : IHandler
{
    private readonly ICartService _cartService;

    public ItemChangedHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public Task Handle(string message)
    {
        var item = JsonSerializer.Deserialize<Item>(message);

        Console.WriteLine($"Processing message for item {item?.Id}");

        //return _cartService.EditItem(item);
        return Task.CompletedTask;
    }
}