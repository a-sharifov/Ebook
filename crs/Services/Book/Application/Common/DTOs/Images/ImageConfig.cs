using Application.Core.MappingConfig.Interfaces;

namespace Application.Common.DTOs.Images;

public sealed class ImageConfig : IMappingConfig
{
    public void Configure() =>
       TypeAdapterConfig<Image, ImageDto>
        .NewConfig()
        .Map(dest => dest.Id, src => src.Id.Value)
        .Map(dest => dest.BucketName, src => src.BucketName.Value)
        .Map(dest => dest.Name, src => src.Name.Value);
}
