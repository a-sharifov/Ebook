using Domain.Core.Results;
using Domain.SharedKernel.Entities;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.Ids;
using Infrastructure.FileStorage.Interfaces;

namespace Infrastructure.FileStorage.Services;

public class ImageService(IFileService fileService) : IImageService
{
    private readonly IFileService _fileService = fileService;

    public async Task DeleteImageAsync(Image image, CancellationToken cancellationToken = default) =>
        await _fileService.DeleteFileAsync(image.BucketName, image.FullName, cancellationToken);


    public async Task<Result<Image>> UploadImageAsync(
        string bucketName,
        Stream imageStream,
        ImageType imageType,
        CancellationToken cancellationToken = default)
    {
        var id = new ImageId(Guid.NewGuid());
        var imageResult = Image.Create(id, bucketName, imageType);

        if (imageResult.IsFailure)
        {
            return imageResult;
        }

        var image = imageResult.Value;

        await _fileService.UploadFileAsync(
            image.BucketName, image.FullName, image.ImageType.Name, imageStream, cancellationToken);

        return image;
    }
}
