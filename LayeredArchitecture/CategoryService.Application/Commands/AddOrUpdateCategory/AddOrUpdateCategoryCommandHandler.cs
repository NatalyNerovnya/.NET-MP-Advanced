using System.Text;
using CategoryService.Application.Exceptions;
using CategoryService.Application.Interfaces;
using CategoryService.Application.Interfaces.Commands;

namespace CategoryService.Application.Commands.AddOrUpdateCategory;

public class AddOrUpdateCategoryCommandHandler : ICommandHandler<AddOrUpdateCategoryCommand>
{
    private readonly IApplicationContext _context;

    public AddOrUpdateCategoryCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(AddOrUpdateCategoryCommand command)
    {
        var validator = new AddOrUpdateCommandValidator();
        var results = await validator.ValidateAsync(command);
        if (!results.IsValid)
        {
            var failures = results.Errors.ToList();
            var message = new StringBuilder();
            failures.ForEach(f => { message.Append(f.ErrorMessage + Environment.NewLine); });
            throw new ValidationException(message.ToString());
        }

        try
        {
            var existedCategory = await _context.GetCategoryById(command.Id);
            await _context.UpdateCategory(command);
        }
        catch (NotExistException e)
        {
            await _context.AddCategory(command);
        }
    }
}