using Domain.Entities;

namespace Application.Common.Interfaces;
public interface IStorageService
{
    Task<string?> UploadAsync(
        Lead lead,
        string containerName,
        CancellationToken cancellationToken);

    Task<Lead?> DownloadToStreamAsync(
        string blobName,
        string containerName,
        string localFilePath,
        CancellationToken cancellationToken);
}