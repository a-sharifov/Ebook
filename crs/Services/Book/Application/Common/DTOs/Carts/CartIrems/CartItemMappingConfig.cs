using Application.Core.MappingConfig.Interfaces;
using Domain.CartAggregate.Entities;

namespace Application.Common.DTOs.Carts.CartIrems;

public sealed class CartItemMappingConfig : IMappingConfig
{
    public void Configure()
    {
        TypeAdapterConfig<CartItem, CartItemDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Book, src => src.Book)
            .Map(dest => dest.Quantity, src => src.Quantity.Value);
    }
}
