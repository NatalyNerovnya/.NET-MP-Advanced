using CatalogService.Api.Models;
using CatalogService.Domain.Models;
using CategoryService.Application.Commands.AddItem;
using CategoryService.Application.Commands.AddOrUpdateCategory;
using CategoryService.Application.Commands.DeleteCategory;
using CategoryService.Application.Commands.DeleteItem;
using CategoryService.Application.Interfaces.Commands;
using CategoryService.Application.Interfaces.Queries;
using CategoryService.Application.Queries.ListCategories;
using CategoryService.Application.Queries.ListItems;
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

    [HttpGet("category", Name = nameof(Get))]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        try
        {
            var result = await _queryDispatcher.Send<ListCategoryQuery, List<Category>>(new ListCategoryQuery());
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("category")]
    public async Task<ActionResult<ResponseWithLinks<object>>> Post(Category category)
    {
        try
        {
            await _commandDispatcher.Send(new AddOrUpdateCategoryCommand()
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image
            });
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

            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("category/{id}", Name = nameof(Delete))]
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
            return BadRequest(e);
        }
    }

    [HttpGet("category/{id}/item", Name = nameof(GetItems))]
    public async Task<ActionResult<IEnumerable<Category>>> GetItems(long id, [FromQuery] int skip, [FromQuery] int limit)
    {
        try
        {
            var result = await _queryDispatcher.Send<ListItemsQuery, List<Item>>(new ListItemsQuery()
            {
                Limit = limit,
                Skip = skip,
                CategoryId = id
            });
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("category/{id}/item")]
    public async Task<ActionResult<IEnumerable<Category>>> GetItems(long id, Item item)
    {
        try
        {
            await _commandDispatcher.Send(
                new AddItemCommand()
                {
                    CategoryId = id,
                    Item = item
                });
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpDelete("category/{id}/item/{itemId}")]
    public async Task<ActionResult<IEnumerable<Category>>> DeleteItem(long id, long itemId)
    {
        try
        {
            await _commandDispatcher.Send(
                new DeleteItemCommand()
                {
                    Id = itemId
                });
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}