using Application.Books.Commands.AddBook;
using Application.Books.Commands.DeleteBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Queries.GetBookById;
using Application.Books.Queries.GetPagedListBooks;
using Contracts.Enum;
using Microsoft.AspNetCore.Authorization;
using Presentation.V1.Books.Models;

namespace Presentation.V1.Books.Controllers;

[Route("api/v{version:apiVersion}/books")]
[ApiVersion("1.0")]
public sealed class BookController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetBooksRequest request)
    {
        var query = new GetPagedListBooksQuery(
            request.PageNumber,
            request.PageSize,
            request.MinPrice,
            request.MaxPrice,
            request.Title,
            request.LanguageId,
            request.AuthorId,
            request.GenreId
            );

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetBookByIdQuery(id);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    //[Authorize(Policy.Admin)]
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddBookRequest request)
    {
        using var stream = request.Poster.OpenReadStream();

        var command = new AddBookCommand(
            request.Title,
            request.Description,
            request.PageCount,
            request.Price,
            request.LanguageId,
            request.Quantity,
            request.AuthorPseudonym,
            request.GenreId,
            request.Poster.FileName,
            stream
            );

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() 
            : HandleFailure(result);
    }

    //[Authorize(Policy.Admin)]
    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateBookRequest request)
    {
        using var stream = request.Poster.OpenReadStream();

        var command = new UpdateBookCommand(
            request.Id,
            request.Title,
            request.Description,
            request.PageCount,
            request.Price,
            request.LanguageId,
            request.Quantity,
            request.AuthorPseudonym,
            request.GenreId,
            request.Poster.FileName,
            stream
            );

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
            : HandleFailure(result);
    }

    //[Authorize(Policy.Admin)]
    [HttpDelete("{id:guid}")] 
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteBookCommand(id);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
            : HandleFailure(result);
    }
}
