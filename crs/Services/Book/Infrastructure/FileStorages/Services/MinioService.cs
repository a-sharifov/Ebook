using Infrastructure.FileStorages.Interfaces;
using Minio;
using Minio.DataModel.Args;
using Contracts.Extensions;
using Microsoft.Extensions.Options;
using Infrastructure.FileStorages.Options;

namespace Infrastructure.FileStorages.Services;

public class MinioService(IMinioClient minioClient, IOptions<BaseUrlOptions> urlOptions) : IFileService
{
    private readonly IMinioClient _minioClient = minioClient;
    private readonly BaseUrlOptions _urlOptions = urlOptions.Value;

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

    public void AddDefaultPolicyBucket(string bucketName)
    {
        string accessPolicy = $@"{{""Version"": ""2012-10-17"",""Statement"": [{{""Action"": [""s3:GetBucketLocation""],""Effect"": ""Allow"",""Principal"": {{""AWS"": [""*""]}},""Resource"": [""arn:aws:s3:::{bucketName}""],""Sid"": """"}},{{""Action"": [""s3:ListBucket""],""Effect"": ""Allow"",""Principal"": {{""AWS"": [""*""]}},""Resource"": [""arn:aws:s3:::{bucketName}""],""Sid"": """"}},{{""Action"": [""s3:GetObject""],""Effect"": ""Allow"",""Principal"": {{""AWS"": [""*""]}},""Resource"": [""arn:aws:s3:::{bucketName}/*""],""Sid"": """"}}]}}";
        
        var args = new SetPolicyArgs()
               .WithBucket(bucketName)
               .WithPolicy(accessPolicy);

        _minioClient.SetPolicyAsync(args).GetAwaiter().GetResult();
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

    public void UploadFile(string bucketName, string fileName, string path)
    {
        var args = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithFileName(path);

        _minioClient.PutObjectAsync(args).GetAwaiter().GetResult();
    }

    public void CreateBucket(string bucketName) => 
        CreateBucketAsync(bucketName).GetAwaiter().GetResult();

    public void UploadFile(string bucketName, string path)
    {
        var fileName = Path.GetFileName(path);
        UploadFile(bucketName, fileName, path);

    }

    public void UploadFilesInBasePath(string bucketName, string path)
    {
        var files = Directory.GetFiles(path);

        foreach (var image in files)
        {
            UploadFile(bucketName, image);
        }
    }

    public string GeneratePermanentUrl(string bucketName, string objectName) =>
        $"{_urlOptions.BaseUrl}/api/v1/buckets/{bucketName}/objects/download?preview=true&prefix={objectName.EncodingBase64()}";
}
