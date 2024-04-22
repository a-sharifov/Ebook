using Application.Core.MappingConfig.Interfaces;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.Ids;
using Domain.SharedKernel.ValueObjects;

namespace Application.Common.DTOs.Images;

public sealed class ImageConfig : IMappingConfig
{
    public void Configure()
    {
        TypeAdapterConfig<Image, ImageDto>
         .NewConfig()
         .Map(dest => dest.Id, src => src.Id.Value)
         .Map(dest => dest.Url, src => src.Url.Value);
    }
}
