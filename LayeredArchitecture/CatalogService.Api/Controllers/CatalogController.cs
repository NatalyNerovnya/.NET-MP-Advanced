using CatalogService.Domain.Models;
using CategoryService.Application.Commands.AddOrUpdateCategory;
using CategoryService.Application.Commands.DeleteCategory;
using CategoryService.Application.Interfaces.Commands;
using CategoryService.Application.Interfaces.Queries;
using CategoryService.Application.Queries.ListCategories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Controllers;

[ApiController]
public class CatalogController: ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public CatalogController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet("category")]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        try
        {
            var result = await _queryDispatcher.Send<ListCategoryQuery, List<Category>>(new ListCategoryQuery());
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost("category")]
    public async Task<ActionResult> Post(Category category)
    {
        try
        {
            await _commandDispatcher.Send(new AddOrUpdateCategoryCommand()
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image
            });
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpDelete("category/{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        try
        {
            await _commandDispatcher.Send(new DeleteCategoryCommand()
            {
                Id = id
            });
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}