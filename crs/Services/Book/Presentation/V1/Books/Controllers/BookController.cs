using Application.Books.Commands.AddBook;
using Application.Books.Commands.DeleteBook;
using Contracts.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Presentation.V1.Books.Models;

namespace Presentation.V1.Books.Controllers;

[Route("api/v{version:apiVersion}/books")]
[ApiVersion("1.0")]
public sealed class BookController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddBookRequest request)
    {
        var command = new AddBookCommand(
            request.Title,
            request.Description,
            request.PageCount,
            request.Price,
            request.LanguageId,
            request.Quantity,
            request.AuthorPseudonym,
            request.GenreId,
            request.Poster.Name,
            request.Poster.OpenReadStream()
            );

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result) 
            : HandleFailure(result);
    }

    [HttpDelete] 
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteBookCommand(id);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(result)
            : HandleFailure(result);
    }

    [Authorize(Policy.User)]
    [HttpGet("test")]
    public IActionResult Test(IFormFile formFile)
    {
        return Ok("Test");
    }
}
