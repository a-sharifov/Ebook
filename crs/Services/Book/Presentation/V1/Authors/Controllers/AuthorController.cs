using Application.Authors.Commands.AddAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Contracts.Enum;
using Microsoft.AspNetCore.Authorization;
using Presentation.V1.Authors.Models;

namespace Presentation.V1.Authors.Controllers;

[Route("api/v{version:apiVersion}/authors")]
[ApiVersion("1.0")]
public class AuthorController(ISender sender) : ApiController(sender)
{
    [Authorize(Policy.Admin)]
    [HttpPost]
    public async Task<IActionResult> Post(AddAuthorRequest request)
    {
        var command = new AddAuthorCommand(request.Pseudonym);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }

    [Authorize(Policy.Admin)]
    [HttpDelete("{authorId:guid}")]
    public async Task<IActionResult> Delete(Guid authorId)
    {
        var command = new DeleteAuthorCommand(authorId);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }
}
