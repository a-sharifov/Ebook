using Application.Genres.Queries.GetGenres;

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
}

