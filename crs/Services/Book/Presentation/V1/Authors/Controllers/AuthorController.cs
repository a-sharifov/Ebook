using Application.Genres.Commands.AddGenre;
using Contracts.Enum;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.V1.Authors.Controllers;

[Route("api/v{version:apiVersion}/authors")]
[ApiVersion("1.0")]
public class AuthorController(ISender sender) : ApiController(sender)
{
    [Authorize(Policy.Admin)]
    [HttpPost]
    public async Task<IActionResult> Post(string name)
    {
        var command = new AddGenreCommand(name);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result); 
    }

}
