using Application.Core.MappingConfig.Interfaces;
using Domain.WishAggregate;

namespace Application.Common.DTOs.Wishes;

public sealed class WishMappingConfig : IMappingConfig
{
    public void Configure() =>
      TypeAdapterConfig<Wish, WishDto>
      .NewConfig()
      .Map(dest => dest.Id, src => src.Id.Value)
      .Map(dest => dest.UserId, src => src.UserId.Value)
      .Map(dest => dest.Items, src => src.Items);
}
