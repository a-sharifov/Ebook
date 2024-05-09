using Application.Authors.Commands.AddAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries.GetAllAuthors;
using Application.Authors.Queries.GetAuthorById;
using Contracts.Enum;
using Microsoft.AspNetCore.Authorization;
using Presentation.V1.Authors.Models;

namespace Presentation.V1.Authors.Controllers;

[Authorize(Policy.Admin)]
[Route("api/v{version:apiVersion}/authors")]
[ApiVersion("1.0")]
public class AuthorController(ISender sender) : ApiController(sender)
{
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetAuthorByIdQuery(id);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
         : HandleFailure(result);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllAuthorsQuery();

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
         : HandleFailure(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddAuthorRequest request)
    {
        var command = new AddAuthorCommand(request.Pseudonym);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteAuthorCommand(id);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
          : HandleFailure(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateAuthorRequest request)
    {
        var command = new UpdateAuthorCommand(
            request.Id, 
            request.Pseudonym);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
         : HandleFailure(result);
    }
}
