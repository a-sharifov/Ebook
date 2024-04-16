using Application.Core.MappingConfig.Interfaces;
using Domain.LanguageAggregate;

namespace Application.Common.DTOs.Languages;

public sealed class LanguageConfig : IMappingConfig
{
    public void Configure() =>
        TypeAdapterConfig<Language, LanguageDto>
        .NewConfig()
        .Map(dest => dest.Id, src => src.Id.Value)
        .Map(dest => dest.Code, src => src.Code.Value)
        .Map(dest => dest.Name, src => src.Name.Value);
}
