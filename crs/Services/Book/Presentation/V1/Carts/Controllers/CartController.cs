using Application.Carts.Commands.AddProductInCart;
using Application.Carts.Queries.GetCart;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.V1.Carts.Controllers;

[Authorize]
[Route("api/v{version:apiVersion}/carts")]
[ApiVersion("1.0")]
public sealed class CartController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var id = GetUserId();
        var query = new GetCartQuery(id);

        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpPut("{bookId:guid}")]
    public async Task<IActionResult> AddBook(Guid bookId)
    {
        var id = GetUserId();
        var command = new AddProductInCartCommand(id, bookId, 1);
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok() 
            : HandleFailure(result);
    }

    //[Authorize]
    //[HttpPut]
    //public async Task<IActionResult> AddBook()
    //{

    //}

    //[HttpPut]
    //public async Task<IActionResult> Clear(Guid id)
    //{

    //}

    //[HttpPut]
    //public async Task<IActionResult> UpdateCount()
    //{

    //}

    //[HttpPut]
    //public async Task<IActionResult> DeleteBook(Guid id)
    //{

    //}
}
