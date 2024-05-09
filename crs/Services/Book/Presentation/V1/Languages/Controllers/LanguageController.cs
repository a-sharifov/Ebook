using Application.Languages.Commands.AddLanguage;
using Application.Languages.Commands.DeleteLanguage;
using Application.Languages.Commands.UpdateLanguage;
using Application.Languages.Queries.GetAllLanguages;
using Application.Languages.Queries.GetLanguageById;
using Contracts.Enum;
using Microsoft.AspNetCore.Authorization;
using Presentation.V1.Languages.Models;

namespace Presentation.V1.Languages.Controllers;

[Route("api/v{version:apiVersion}/languages")]
[ApiVersion("1.0")]
public sealed class LanguageController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllLanguagesQuery();

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
           : HandleFailure(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetLanguageByIdQuery(id);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
           : HandleFailure(result);
    }

    [Authorize(Policy.Admin)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddLanquageRequest request)
    {
        var command = new AddLanquageCommand(request.Name, request.Code);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
           : HandleFailure(result);
    }

    [Authorize(Policy.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteLanguageCommand(id);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
           : HandleFailure(result);
    }

    [Authorize(Policy.Admin)]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLanguageRequest request)
    {
        var command = new UpdateLanguageCommand(
            request.Id,
            request.Name,
            request.Code);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
           : HandleFailure(result);
    }
}
