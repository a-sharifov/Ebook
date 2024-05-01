using Application.Wishes.Commands.AddBookInWish;
using Application.Wishes.Commands.DeleteBookInWish;
using Application.Wishes.Queries.BookInWIsh;
using Application.Wishes.Queries.GetWish;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.V1.Wishes.Controllers;

[Authorize]
[Route("api/v{version:apiVersion}/wisher")]
[ApiVersion("1.0")]
public sealed class WishController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = GetUserId();
        var query = new GetWishQuery(userId);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpPut("{bookId:guid}")]
    public async Task<IActionResult> AddBook(Guid bookId)
    {
        var userId = GetUserId();
        var command = new AddBookInWishCommand(userId, bookId);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }

    [HttpDelete("{bookId:guid}")]
    public async Task<IActionResult> DeleteBook(Guid bookId)
    {
        var userId = GetUserId();
        var command = new DeleteBookInWishCommand(userId, bookId);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }

    [HttpGet("book-in-wish/{bookId:guid}")]
    public async Task<IActionResult> InWish(Guid bookId)
    {
        var userId = GetUserId();
        var query = new BookInWishQuery(userId, bookId);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
          : HandleFailure(result);
    }
}
