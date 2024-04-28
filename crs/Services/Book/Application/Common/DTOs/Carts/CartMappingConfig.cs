using Application.Core.MappingConfig.Interfaces;
using Domain.CartAggregate;

namespace Application.Common.DTOs.Carts;

public sealed class CartMappingConfig : IMappingConfig
{
    public void Configure() =>
        TypeAdapterConfig<Cart, CartDto>
       .NewConfig()
       .Map(dest => dest.Id, src => src.Id.Value)
       .Map(dest => dest.Items, src => src.Items)
       .Map(dest => dest.TotalPrice, src => src.TotalPrice);
}

