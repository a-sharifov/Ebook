using Application.Core.MappingConfig.Interfaces;
using Domain.WishAggregate.Entities;

namespace Application.Common.DTOs.Wishes.WishItems;

public sealed class WishItemMappingConfig : IMappingConfig
{
    public void Configure() =>
      TypeAdapterConfig<WishItem, WishItemDto>
      .NewConfig()
      .Map(dest => dest.Id, src => src.Id.Value)
      .Map(dest => dest.Book, src => src.Book);
}
