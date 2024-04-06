using Infrastructure.FileStorage.Interfaces;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.FileStorage.Services;

public class MinioService(IMinioClient minioClient) : IFileService
{
    private readonly IMinioClient _minioClient = minioClient;

    public async Task UploadFileAsync(
        string bucketName,
        string fileName,
        string fileType,
        Stream fileStream,
        CancellationToken cancellationToken = default)
    {
        var args = new PutObjectArgs();
        args.WithBucket(bucketName);
        args.WithContentType(fileType);
        args.WithStreamData(fileStream);
        args.WithFileName(fileType);

        await _minioClient.PutObjectAsync(args, cancellationToken);
    }

    public async Task DeleteFileAsync(
        string bucketName,
        string objectName,
        CancellationToken cancellationToken = default)
    {
        var args = new RemoveObjectArgs();
        args.WithBucket(bucketName);
        args.WithObject(objectName);

        await _minioClient.RemoveObjectAsync(args, cancellationToken);
    }
}
