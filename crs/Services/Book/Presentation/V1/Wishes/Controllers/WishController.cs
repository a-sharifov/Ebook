using Application.Books.Queries.GetBook;
using Application.Wishes.Commands.AddBookInWish;
using Application.Wishes.Commands.DeleteBookInWish;
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
        var id = GetUserId();
        var query = new GetWishQuery(id);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpPut("{bookId:guid}")]
    public async Task<IActionResult> AddBook(Guid bookId)
    {
        var id = GetUserId();
        var command = new AddBookInWishCommand(id, bookId);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }

    [HttpDelete("{bookId:guid}")]
    public async Task<IActionResult> DeleteBook(Guid bookId)
    {
        var id = GetUserId();
        var command = new DeleteBookInWishCommand(id, bookId);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }
}
