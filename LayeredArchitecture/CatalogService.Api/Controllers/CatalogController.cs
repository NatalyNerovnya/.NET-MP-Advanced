using CatalogService.Api.CustomAttributes;
using CatalogService.Api.Models;
using CatalogService.Domain.Models;
using CategoryService.Application.Commands.AddItem;
using CategoryService.Application.Commands.AddOrUpdateCategory;
using CategoryService.Application.Commands.DeleteCategory;
using CategoryService.Application.Commands.DeleteItem;
using CategoryService.Application.Commands.UpdateItem;
using CategoryService.Application.Interfaces.Commands;
using CategoryService.Application.Interfaces.Queries;
using CategoryService.Application.Queries.GetCategory;
using CategoryService.Application.Queries.ListCategories;
using CategoryService.Application.Queries.ListItems;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Controllers;

[ApiController]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ActionResult))]
[ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ActionResult))]
[ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ActionResult))]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ActionResult))]
public class CatalogController: ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public CatalogController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet("categories", Name = nameof(Get))]
    [Authorize("Read")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<IEnumerable<Category>>))]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        var result = await _queryDispatcher.Send<ListCategoryQuery, List<Category>>(new ListCategoryQuery());
        return Ok(result);
    }

    [HttpGet("categories/{id}", Name = nameof(GetCategory))]
    [Authorize("Read")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<Category>))]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var result = await _queryDispatcher.Send<GetCategoryQuery, Category>(new GetCategoryQuery()
        {
            Id = id
        });

        return Ok(result);
    }

    [HttpPost("categories")]
    [Authorize("ReadWrite")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ActionResult<ResponseWithLinks<object>>))]
    public async Task<ActionResult<ResponseWithLinks<object>>> Post(Category category)
    {
        await _commandDispatcher.Send(new AddOrUpdateCategoryCommand()
        {
            Id = category.Id,
            Name = category.Name,
            Image = category.Image
        });
        var createdCategoryLink = Url.Link(nameof(GetCategory), new { id = category.Id });
        var response = new ResponseWithLinks<object>()
        {
            Body = { },
            Links = new List<Link>()
            {
                new()
                {
                    Href = Url.Link(nameof(Get), new {} ) ?? "unknown",
                    Method = "GET",
                    Rel = "get_all"
                },
                new()
                {
                    Href = Url.Link(nameof(Delete), new { id = category.Id}) ?? "unknown",
                    Method = "DELETE",
                    Rel = "delete"
                },
                new()
                {
                    Href = Url.Link(nameof(GetItems), new { id = category.Id}) ?? "unknown",
                    Method = "GET",
                    Rel = "get_items"
                }
            }
        };

        return Created(createdCategoryLink, response);
    }

    [HttpDelete("categories/{id}", Name = nameof(Delete))]
    [Authorize("ReadWrite")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ActionResult))]
    public async Task<ActionResult> Delete(long id)
    {
        await _commandDispatcher.Send(new DeleteCategoryCommand()
        {
            Id = id
        });
        return NoContent();
    }

    [HttpGet("categories/{id}/items", Name = nameof(GetItems))]
    [Authorize("Read")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult<IEnumerable<Item>>))]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems(long id, [FromQuery] int skip, [FromQuery] int limit)
    {
        var result = await _queryDispatcher.Send<ListItemsQuery, List<Item>>(new ListItemsQuery()
        {
            Limit = limit,
            Skip = skip,
            CategoryId = id
        });

        return Ok(result);
    }

    [HttpPost("categories/{id}/items")]
    [Authorize("ReadWrite")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ActionResult))]
    public async Task<ActionResult> AddItem(long id, Item item)
    {
        item.CategoryId = id;
        await _commandDispatcher.Send<AddItemCommand>((item as AddItemCommand)!);

        return Ok();
    }

    [HttpDelete("categories/{id}/items/{itemId}")]
    [Authorize("ReadWrite")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ActionResult))]
    public async Task<ActionResult> DeleteItem(long id, long itemId)
    {
        await _commandDispatcher.Send(
            new DeleteItemCommand()
            {
                Id = itemId
            });

        return NoContent();
    }

    [HttpPut("categories/{id}/items/{itemId}")]
    [Authorize("ReadWrite")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionResult))]
    public async Task<ActionResult> UpdateItem(long id, long itemId, UpdateItemModel item)
    {
        await _commandDispatcher.Send(
            new UpdateItemCommand()
            {
                CategoryId = id,
                Id = itemId,
                Name = item.Name,
                Price = item.Price,
                Description = item.Description,
                Amount = item.Quantity
                
            });

        return Ok();
    }
}