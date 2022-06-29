using CartingService.BLL;
using CartingService.Entities.Exceptions;
using CartingService.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Api.Controllers;

[ApiController]
public class CartController: ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{id}")]
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

    [HttpPost("{id}/item")]
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
}