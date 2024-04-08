using Application.Core.MappingConfig.Interfaces;
using Domain.GenreAggregate;

namespace Application.Common.DTOs.Genres;

public sealed class GenreMappingConfig : IMappingConfig
{
    public void Configure() => 
        TypeAdapterConfig<Genre, GenreDto>
        .NewConfig()
        .Map(dest => dest.Id, src => src.Id.Value)
        .Map(dest => dest.Image, src => src.Image.Path)
        .Map(dest => dest.Name, src => src.Name.Value);
}
