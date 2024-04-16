using Application.Languages.Queries.GetLanguages;

namespace Presentation.V1.Languages.Controllers;

[Route("api/v{version:apiVersion}/languages")]
[ApiVersion("1.0")]
public sealed class LanguageController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetLanguagesQuery();

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
           : HandleFailure(result);
    }

}
