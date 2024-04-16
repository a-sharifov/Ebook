namespace Presentation.V1.Books.Controllers;

[Route("api/v{version:apiVersion}/books")]
[ApiVersion("1.0")]
public sealed class BookController(ISender sender) : ApiController(sender)
{
    //[HttpPost]
    //public 
    
}
