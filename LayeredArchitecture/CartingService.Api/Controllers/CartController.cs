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
    private readonly ILogger<CartController> _logger;

    public CartController(ICartService cartService, ILogger<CartController> logger)
    {
        _cartService = cartService;
        _logger = logger;
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
        _logger.LogInformation("Action started: Get cart items v2");
        if (!int.TryParse(id, out var validId))
        {
            _logger.LogError($"Id {id} is of invalid format");
            return BadRequest();
        }

        try
        {
            var items = await _cartService.GetAllItems(validId);
            return Ok(items);
        }
        catch (CartNotFoundException e)
        {
            _logger.LogError($"Cart with {id} is not found");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error during cart retrieval: {e.Message}");
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
        _logger.LogInformation("Action started: Get cart items v1");

        if (!int.TryParse(id, out var validId))
        {
            _logger.LogError($"Id {id} is of invalid format");
            return BadRequest();
        }

        try
        {
            var cart = await _cartService.GetCartById(validId);
            return Ok(cart);
        }
        catch (CartNotFoundException e)
        {
            _logger.LogError($"Cart with {id} is not found");
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error during cart retrieval: {e.Message}");
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
        _logger.LogInformation("Action started: Add item to cart");

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
            _logger.LogError($"Cart {cartId} doesn't exists.");
            return NotFound($"Cart {cartId} doesn't exists.");
        }
        catch (ItemDuplicateException e)
        {
            _logger.LogError($"Item with id = {item.Id} already exists");
            return BadRequest($"Item with id = {item.Id} already exists");
        }
        catch (ItemNotValidException e)
        {
            _logger.LogError($"Item is not valid");
            return BadRequest($"Item is not valid");
        }
        catch (Exception e)
        {
            _logger.LogError($"Error during item adding: {e.Message}");
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
        _logger.LogInformation("Action started: Remove item from cart");

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
            _logger.LogError($"Error during item removing: {e.Message}");
            return BadRequest(e);
        }
    }
}