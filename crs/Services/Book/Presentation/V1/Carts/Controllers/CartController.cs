using Application.Carts.Commands.AddBookInCart;
using Application.Carts.Commands.DeleteItemInCart;
using Application.Carts.Commands.UpdateQuantityBookInCart;
using Application.Carts.Queries.BookInCart;
using Application.Carts.Queries.GetCart;
using Application.Carts.Queries.GetQuantityBookCart;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.V1.Carts.Controllers;

[Authorize]
[Route("api/v{version:apiVersion}/carts")]
[ApiVersion("1.0")]
public sealed class CartController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = GetUserId();
        var query = new GetCartQuery(userId);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }


    [HttpGet("book-in-cart/{bookId:guid}")]
    public async Task<IActionResult> BookInCart(Guid bookId)
    {
        var userId = GetUserId();
        var query = new BookInCartQuery(userId, bookId);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpGet("book-quantity")]
    public async Task<IActionResult> GetBookQuantity()
    {
        var userId = GetUserId();
        var query = new GetQuantityCartBookQuery(userId);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpPost("{bookId:guid}")]
    public async Task<IActionResult> AddBook(Guid bookId)
    {
        var userId = GetUserId();
        var command = new AddProductInCartCommand(userId, bookId);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
            : HandleFailure(result);
    }

    [HttpPut("items/{itemId:guid}")]
    public async Task<IActionResult> UpdateQuantityBookItem(Guid itemId, [FromBody, Required] int quantity)
    {
        var command = new UpdateQuantityBookInCart(itemId, quantity);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }

    [HttpDelete("items/{itemId:guid}")]
    public async Task<IActionResult> DeleteItemInCart(Guid itemId)
    {
        var command = new DeleteItemInCartCommand(itemId);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }
}
