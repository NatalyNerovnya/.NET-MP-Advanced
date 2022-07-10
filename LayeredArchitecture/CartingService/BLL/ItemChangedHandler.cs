using System.Text.Json;
using CartingService.Entities.Models;
using NotificationClient.Interfaces;

namespace CartingService.BLL;

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
        return _cartService.EditItem(item);
    }
}