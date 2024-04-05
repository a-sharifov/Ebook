using Microsoft.Extensions.FileProviders;
using Presentation.Genres.V1.Models;

namespace Presentation.Genres.V1.Controllers;

[Route("api/v{version:apiVersion}/genres")]
[ApiVersion("1.0")]
public sealed class GenreController(ISender sender) : ApiController(sender)
{
    //[HttpGet("{id}")]
    //public async Task<IActionResult> Get(Guid id)
    //{

    //}

    //[HttpGet]
    //public async Task<IActionResult> Get()
    //{

    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(Guid id)
    //{

    //}

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateImage([FromBody] IFileInfo genreImage, Guid id)
    //{
        
    //}

    //[HttpPost]
    //public async Task<IActionResult> Add([FromBody] AddGenreRequest request)
    //{

    //}
}
