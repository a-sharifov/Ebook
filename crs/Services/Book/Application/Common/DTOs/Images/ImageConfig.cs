using Application.Core.MappingConfig.Interfaces;
using Domain.SharedKernel.Entities;

namespace Application.Common.DTOs.Images;

public sealed class ImageConfig : IMappingConfig
{
    public void Configure() =>
       TypeAdapterConfig<Image, ImageDto>
        .NewConfig()
        .Map(dest => dest.Id, src => src.Id.Value)
        .Map(dest => dest.ImageUrl, src => src.ImageUrl.Value);
}
