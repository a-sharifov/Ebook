﻿namespace Infrastructure.FileStorage.Interfaces;

public interface IFileService
{
    Task UploadFileAsync(string bucketName, string fileName, string fileType, Stream fileStream, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string bucketName, string objectName, CancellationToken cancellationToken = default);
}