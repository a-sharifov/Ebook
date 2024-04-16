using Application.Common.DTOs.Images;

namespace Application.Images.Commands.AddImage;

public sealed record AddImageCommand(
    string BucketName,
    Stream ImageStream,
    string Name
    ) : ICommand<ImageDto>;
