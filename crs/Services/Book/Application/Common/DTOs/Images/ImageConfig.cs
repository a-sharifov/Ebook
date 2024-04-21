using Application.Core.MappingConfig.Interfaces;
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

        TypeAdapterConfig<ImageDto, Image>
        .NewConfig()
        .Map(dest => dest.Id, src => new ImageId(src.Id))
        .Map(dest => dest.Url, src => ImageUrl.Create( src.Url).Value);
    }
}
