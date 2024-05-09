using Application.Genres.Commands.AddGenre;
using Application.Genres.Commands.DeleteGenre;
using Application.Genres.Commands.UpdateGenre;
using Application.Genres.Queries.GetAllGenres;
using Application.Genres.Queries.GetGenreById;
using Contracts.Enum;
using Microsoft.AspNetCore.Authorization;
using Presentation.V1.Genres.Models;

namespace Presentation.V1.Genres.Controllers;

[Authorize(Policy.Admin)]
[Route("api/v{version:apiVersion}/genres")]
[ApiVersion("1.0")]
public sealed class GenreController(ISender sender) : ApiController(sender)
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllGenresQuery();

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetGenreByIdQuery(id);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddGenreRequest request)
    {
        var command = new AddGenreCommand(request.Name);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }

    [HttpPut]
    public async Task<IActionResult> Post([FromBody] UpdateGenreRequest request)
    {
        var command = new UpdateGenreCommand(
            request.Id, 
            request.Name);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteGenreCommand(id);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }
}

