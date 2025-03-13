//using MediatR;
//using Order.Grpc;
//using Application.Orders.Queries.GetOrderById;
//using Application.Orders.Queries.GetOrdersByUserId;
//using Application.Orders.Commands.CreateOrder;
//using Application.Orders.Commands.UpdateOrderStatus;
//using Application.Orders.Commands.CancelOrder;
//using Domain.OrderAggregate.ValueObjects;
//using Grpc.Core;
//using Orders.Protobuf;

//namespace Api.Services;

//public class OrderGrpcService : OrderService.OrderServiceBase
//{
//    private readonly IMediator _mediator;

//    public OrderGrpcService(IMediator mediator)
//    {
//        _mediator = mediator;
//    }

//    public override async Task<OrderResponse> GetOrder(GetOrderRequest request, ServerCallContext context)
//    {
//        var query = new GetOrderByIdQuery(request.OrderId);
//        var result = await _mediator.Send(query);

//        if (result.IsFailure)
//        {
//            throw new RpcException(new Status(StatusCode.NotFound, result.Error.Message));
//        }

//        var order = result.Value;
//        return MapToOrderResponse(order);
//    }

//    public override async Task<OrdersResponse> GetOrdersByUser(GetOrdersByUserRequest request, ServerCallContext context)
//    {
//        var query = new GetOrdersByUserIdQuery(Guid.Parse(request.UserId));
//        var result = await _mediator.Send(query);

//        if (result.IsFailure)
//        {
//            throw new RpcException(new Status(StatusCode.NotFound, result.Error.Message));
//        }

//        var orders = result.Value;
//        var response = new OrdersResponse();
//        foreach (var order in orders)
//        {
//            response.Orders.Add(MapToOrderResponse(order));
//        }

//        return response;
//    }

//    public override async Task<OrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
//    {
//        var items = request.Items.Select(item => new CreateOrderItemDto(
//            item.BookId,
//            item.BookTitle,
//            item.Quantity,
//            (decimal)item.Price
//        )).ToList();

//        var shippingAddress = new CreateOrderShippingAddressDto(
//            request.ShippingAddress.Street,
//            request.ShippingAddress.City,
//            request.ShippingAddress.State,
//            request.ShippingAddress.Country,
//            request.ShippingAddress.ZipCode
//        );

//        var command = new CreateOrderCommand(
//            Guid.Parse(request.UserId),
//            items,
//            shippingAddress
//        );

//        var result = await _mediator.Send(command);

//        if (result.IsFailure)
//        {
//            throw new RpcException(new Status(StatusCode.InvalidArgument, result.Error.Message));
//        }

//        var order = result.Value;
//        return MapToOrderResponse(order);
//    }

//    public override async Task<OrderResponse> UpdateOrderStatus(UpdateOrderStatusRequest request, ServerCallContext context)
//    {
//        OrderStatus orderStatus;
//        if (!Enum.TryParse<OrderStatus>(request.Status, true, out orderStatus))
//        {
//            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid order status"));
//        }

//        var command = new UpdateOrderStatusCommand(request.OrderId, orderStatus);
//        var result = await _mediator.Send(command);

//        if (result.IsFailure)
//        {
//            throw new RpcException(new Status(StatusCode.InvalidArgument, result.Error.Message));
//        }

//        var order = result.Value;
//        return MapToOrderResponse(order);
//    }

//    public override async Task<OrderResponse> CancelOrder(CancelOrderRequest request, ServerCallContext context)
//    {
//        var command = new CancelOrderCommand(request.OrderId);
//        var result = await _mediator.Send(command);

//        if (result.IsFailure)
//        {
//            throw new RpcException(new Status(StatusCode.InvalidArgument, result.Error.Message));
//        }

//        var order = result.Value;
//        return MapToOrderResponse(order);
//    }

//    private OrderResponse MapToOrderResponse(Domain.OrderAggregate.Order order)
//    {
//        var response = new OrderResponse
//        {
//            OrderId = order.Id.Value.ToString(),
//            UserId = order.UserId.ToString(),
//            OrderDate = order.OrderDate.Value.ToString("o"),
//            Status = order.Status.ToString(),
//            TotalAmount = (double)order.TotalAmount,
//            ShippingAddress = new ShippingAddressDto
//            {
//                Street = order.ShippingAddress.Street,
//                City = order.ShippingAddress.City,
//                State = order.ShippingAddress.State,
//                Country = order.ShippingAddress.Country,
//                ZipCode = order.ShippingAddress.ZipCode
//            }
//        };

//        foreach (var item in order.Items)
//        {
//            response.Items.Add(new OrderItemDto
//            {
//                BookId = item.BookId,
//                BookTitle = item.BookTitle,
//                Quantity = item.Quantity.Value,
//                Price = (double)item.Price
//            });
//        }

//        return response;
//    }
//}
