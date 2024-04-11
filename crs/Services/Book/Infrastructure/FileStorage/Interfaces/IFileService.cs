namespace Infrastructure.FileStorage.Interfaces;

public interface IFileService
{
    Task UploadFileAsync(string bucketName, string fileName, Stream fileStream, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string bucketName, string objectName, CancellationToken cancellationToken = default);
    Task<string> GetUrl(string bucketName, string fileName, int ttl);
    Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default);
    Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default);
}
    