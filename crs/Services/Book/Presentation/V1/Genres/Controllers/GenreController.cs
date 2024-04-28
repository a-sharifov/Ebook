using Application.Genres.Commands.AddGenre;
using Application.Genres.Commands.DeleteGenre;
using Application.Genres.Queries.GetGenres;
using Contracts.Enum;
using Microsoft.AspNetCore.Authorization;
using Presentation.V1.Genres.Models;

namespace Presentation.V1.Genres.Controllers;

[Route("api/v{version:apiVersion}/genres")]
[ApiVersion("1.0")]
public sealed class GenreController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetGenresQuery();

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [Authorize(Policy.Admin)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddGenreRequest request)
    {
        var command = new AddGenreCommand(request.Name);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }

    [Authorize(Policy.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteGenreCommand(id);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }
}

