using Application.Core.MappingConfig.Interfaces;
using Domain.AuthorAggregate;

namespace Application.Common.DTOs.Authors;

public sealed class AuthorMappingConfig : IMappingConfig
{
    public void Configure() => 
        TypeAdapterConfig<Author, AuthorDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.FirstName, src => src.FirstName.Value)
            .Map(dest => dest.LastName, src => src.LastName.Value)
            .Map(dest => dest.Pseudonym, src => src.Pseudonym)
            .Map(dest => dest.Description, src => src.Description.Value)
            .Map(dest => dest.Image, src => src.Image.Path);

}
