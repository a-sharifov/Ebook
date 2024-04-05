using Application.Core.MappingConfig.Interfaces;
using Domain.UserAggregate.Entities;

namespace Application.Common.DTOs.Users.Wishs;

public sealed class WishMappingConfig : IMappingConfig
{
    public void Configure() =>
       TypeAdapterConfig<Wish, WishDto>
       .NewConfig()
       .Map(dest => dest.Id, src => src.Id)
       .Map(dest => dest.Book, src => src.Book);
}
