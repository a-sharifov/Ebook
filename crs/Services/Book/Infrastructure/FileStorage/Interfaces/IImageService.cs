using Domain.Core.Results;
using Domain.SharedKernel.Entities;
using Domain.SharedKernel.Enumerations;

namespace Infrastructure.FileStorage.Interfaces;

public interface IImageService
{
    public Task<Result<Image>> UploadImageAsync(string bucketName, Stream imageStream, ImageType imageType, CancellationToken cancellationToken = default);
    public Task DeleteImageAsync(Image image, CancellationToken cancellationToken = default);
}
