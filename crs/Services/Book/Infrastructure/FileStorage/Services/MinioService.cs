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
        Stream fileStream,
        CancellationToken cancellationToken = default)
    {
        var args = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithStreamData(fileStream)
            .WithFileName(fileName);

        await _minioClient.PutObjectAsync(args, cancellationToken);
    }

    public async Task DeleteFileAsync(
        string bucketName,
        string objectName,
        CancellationToken cancellationToken = default)
    {
        var args = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);

         await _minioClient.RemoveObjectAsync(args, cancellationToken);
    }

    public async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        var args = new MakeBucketArgs()
            .WithBucket(bucketName);

        await _minioClient.MakeBucketAsync(args, cancellationToken);
    }

    public async Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        var args = new RemoveBucketArgs()
            .WithBucket(bucketName);

        await _minioClient.RemoveBucketAsync(args, cancellationToken);
    }

    public async Task<string> GetUrl(
        string bucketName,
        string fileName,
        int ttl)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithExpiry(ttl);

        var url = await _minioClient.PresignedGetObjectAsync(args);
        return url;
    }
}
