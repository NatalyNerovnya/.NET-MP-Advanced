using System.Text.Json;
using CategoryService.Application.Interfaces;
using CategoryService.Application.Interfaces.Commands;
using NotificationClient.Interfaces;

namespace CategoryService.Application.Commands.UpdateItem;

public class UpdateItemCommandHandler : ICommandHandler<UpdateItemCommand>
{
    private readonly IApplicationContext _applicationContext;
    private readonly IPublisherClient _publisherClient;

    public UpdateItemCommandHandler(IApplicationContext applicationContext, IPublisherClient publisherClient)
    {
        _applicationContext = applicationContext;
        _publisherClient = publisherClient;
    }

    public async Task Handle(UpdateItemCommand command)
    {
        var existedItem = await _applicationContext.GetItem(command.Id);
        if (command.Name is not null)
        {
            existedItem.Name = command.Name;
        }
        if (command.Description is not null)
        {
            existedItem.Description = command.Description;
        }
        if (command.Price is not null)
        {
            existedItem.Price = command.Price;
        }
        if (command.Amount is not null)
        {
            existedItem.Amount = command.Amount;
        }

        await _applicationContext.UpdateItem(existedItem);
        await _publisherClient.Publish(JsonSerializer.Serialize(command));
    }
}