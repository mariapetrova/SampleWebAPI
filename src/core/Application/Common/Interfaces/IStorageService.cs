using Domain.Entities;

namespace Application.Common.Interfaces;
public interface IStorageService
{
    Task<string?> UploadAsync(
        Person person,
        string containerName,
        CancellationToken cancellationToken);

    Task<Person?> DownloadToStreamAsync(
        string blobName,
        string containerName,
        string localFilePath,
        CancellationToken cancellationToken);
}