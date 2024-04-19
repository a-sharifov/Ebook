namespace Infrastructure.FileStorages.Interfaces;

public interface IFileService
{
    Task UploadFileAsync(string bucketName, string fileName, Stream fileStream, CancellationToken cancellationToken = default);
    void UploadFile(string bucketName, string fileName, string path);
    void UploadFile(string bucketName, string path);
    void UploadFilesInBasePath(string bucketName, string path);
    Task DeleteFileAsync(string bucketName, string objectName, CancellationToken cancellationToken = default);
    Task<string> GetUrl(string bucketName, string fileName, int ttl);
    Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken = default);
    void CreateBucket(string bucketName);
    Task DeleteBucketAsync(string bucketName, CancellationToken cancellationToken = default);
    void AddDefaultPolicyBucket(string bucketName);
    string GeneratePermanentUrl(string bucketName, string objectName);
}
    