using Application.Carts.Commands.AddBookInCart;
using Application.Carts.Commands.DeleteBookInCart;
using Application.Carts.Queries.BookInCart;
using Application.Carts.Queries.GetCart;
using Application.Carts.Queries.GetQuantityBookCart;
using Microsoft.AspNetCore.Authorization;
using System.Reactive.Subjects;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

    [HttpPut("{bookId:guid}")]
    public async Task<IActionResult> AddBook(Guid bookId)
    {
        var userId = GetUserId();
        var command = new AddProductInCartCommand(userId, bookId);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() 
            : HandleFailure(result);
    }

    //[HttpPut]
    //public async Task<IActionResult> Clear(Guid id)
    //{

    //}

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

    [HttpDelete("delete-item/{itemId:guid}")]
    public async Task<IActionResult> DeleteItemInCart(Guid itemId)
    {
        var command = new DeleteItemInCartCommand(itemId);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }
}
