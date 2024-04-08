using Application.Core.MappingConfig.Interfaces;

namespace Application.Common.DTOs.Images;

public sealed class ImageConfig : IMappingConfig
{
    public void Configure() =>
       TypeAdapterConfig<Image, ImageDto>
        .NewConfig()
        .Map(dest => dest.Id, src => src.Id.Value)
        .Map(dest => dest.BucketName, src => src.BucketName)
        .Map(dest => dest.ImageName, src => src.ImageName)
        .Map(dest => dest.ImageType, src => src.ImageType);
}
