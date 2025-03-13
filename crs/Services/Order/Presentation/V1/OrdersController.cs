using Application.Orders.Commands.CancelOrder;
using Application.Orders.Commands.CreateOrder;
using Application.Orders.Commands.UpdateOrderStatus;
using Application.Orders.Queries.GetOrderById;
using Application.Orders.Queries.GetOrdersByUserId;
using Contracts.Enum;
using Presentation.Core.Controllers;
using Presentation.V1.Models;

namespace Presentation.V1;

[ApiController]
[Route("api/v{version:apiVersion}/orders")]
[ApiVersion("1.0")]
public class OrdersController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    [Authorize(Policy = Policy.Admin)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var command = new CreateOrderCommand(
            request.UserId,
            request.Street,
            request.City,
            request.State,
            request.Country,
            request.ZipCode,
            request.Items.Select(item => new CreateOrderItemCommand(
                item.BookId,
                item.BookTitle,
                item.UnitPrice,
                item.Quantity
            )).ToList()
        );
        
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
                 : HandleFailure(result);
    }

    [HttpGet("{orderId:guid}")]
    [Authorize(Policy = Policy.UserAndAdmin)]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        var query = new GetOrderByIdQuery(orderId);
        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpGet("user/{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetOrdersByUserId(Guid userId)
    {
        var query = new GetOrdersByUserIdQuery(userId);
        var result = await _sender.Send(query);

        return result.IsSuccess ? Ok(result.Value)
            : HandleFailure(result);
    }

    [HttpPut("{orderId:guid}/status")]
    [Authorize(Policy = Policy.Admin)]
    public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusRequest request)
    {
        var command = new UpdateOrderStatusCommand(orderId, request.Status);
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
            : HandleFailure(result);
    }

    [HttpPost("{orderId:guid}/cancel")]
    [Authorize]
    public async Task<IActionResult> CancelOrder(Guid orderId)
    {
        var command = new CancelOrderCommand(orderId);
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok()
            : HandleFailure(result);
    }
}
