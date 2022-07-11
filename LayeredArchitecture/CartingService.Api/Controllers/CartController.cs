using CartingService.BLL;
using CartingService.Entities.Exceptions;
using CartingService.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[ApiVersion("2")]
[Route("v{v:apiVersion}/[controller]")]
public class CartController: ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }
    
    /// <summary>
    /// GetItems v2
    /// </summary>
    /// <param name="id">cart id</param>
    /// <returns>items </returns>
    [HttpGet("{id}")]
    [MapToApiVersion("2")]
    public async Task<ActionResult<IEnumerable<Item>>> GetV2(string id)
    {
        if (!int.TryParse(id, out var validId))
        {
            return BadRequest();
        }

        try
        {
            var items = await _cartService.GetAllItems(validId);
            return Ok(items);
        }
        catch (CartNotFoundException e)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    /// <summary>
    /// GetItems v1
    /// </summary>
    /// <param name="id">cart id</param>
    /// <returns>cart with items</returns>
    [HttpGet("{id}")]
    [MapToApiVersion("1")]
    public async Task<ActionResult<Cart>> Get(string id)
    {
        if (!int.TryParse(id, out var validId))
        {
            return BadRequest();
        }

        try
        {
            var cart = await _cartService.GetCartById(validId);
            return Ok(cart);
        }
        catch (CartNotFoundException e)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    /// <summary>
    /// Add item to cart
    /// </summary>
    /// <param name="id">cart id</param>
    /// <param name="item">item</param>
    /// <returns></returns>
    [HttpPost("{id}/items")]
    public async Task<ActionResult> AddItem(string id, Item item)
    {
        if (!int.TryParse(id, out var cartId))
        {
            return BadRequest();
        }

        try
        {
            await _cartService.AddItem(cartId, item);
            return Ok($"Item was added to cart {cartId}");
        }
        catch (CartNotFoundException e)
        {
            return NotFound($"Cart {cartId} doesn't exists.");
        }
        catch (ItemDuplicateException e)
        {
            return BadRequest($"Item with id = {item.Id} already exists");
        }
        catch (ItemNotValidException e)
        {
            return BadRequest($"Item is not valid");
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    /// <summary>
    /// Remove item from cart
    /// </summary>
    /// <param name="id">cart id</param>
    /// <param name="itemId">item id</param>
    /// <returns></returns>
    [HttpDelete("{id}/items/{itemId}")]
    public async Task<ActionResult> RemoveItem(string id, string itemId)
    {
        if (!int.TryParse(id, out var cartId) || !int.TryParse(itemId, out var parsedItemId))
        {
            return BadRequest();
        }

        try
        {
            await _cartService.RemoveItem(cartId, parsedItemId);
            return Ok($"Item {itemId} was deleted from cart {cartId}");
        }
        catch (CartNotFoundException e)
        {
            return NotFound($"Cart {id} doesn't exists.");
        }
        catch (ItemNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}