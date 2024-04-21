using Application.Core.MappingConfig.Interfaces;
using Domain.AuthorAggregate;

namespace Application.Common.DTOs.Authors;

public sealed class AuthorMappingConfig : IMappingConfig
{
    public void Configure() => 
        TypeAdapterConfig<Author, AuthorDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Pseudonym, src => src.Pseudonym);
}
