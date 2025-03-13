using Application.Core.MappingConfig.Interfaces;
using Application.Orders.Common.DTOs;
using Domain.OrderAggregate.Entities;

namespace Application.Orders.Common.Mappings;

public sealed class OrderMappingConfig : IMappingConfig
{
    public void Configure()
    {
        TypeAdapterConfig<Order, OrderDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.OrderDate, src => src.OrderDate.Value)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.ShippingAddress, src => src.ShippingAddress.ToString())
            .Map(dest => dest.TotalAmount, src => src.TotalAmount)
            .Map(dest => dest.Items, src => src.Items.Adapt<List<OrderItemDto>>());

        TypeAdapterConfig<OrderItem, OrderItemDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.BookId, src => src.BookId)
            .Map(dest => dest.BookTitle, src => src.BookTitle)
            .Map(dest => dest.UnitPrice, src => src.UnitPrice)
            .Map(dest => dest.Quantity, src => src.Quantity.Value)
            .Map(dest => dest.TotalPrice, src => src.TotalPrice);
    }
}
