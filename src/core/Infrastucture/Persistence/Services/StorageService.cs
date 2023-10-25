using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Application.Common.Interfaces;
using System.Text;
using Newtonsoft.Json;
using Domain.Entities;

namespace Infrastructure.Persistence.Services;
public class StorageService : IStorageService
{
    private readonly BlobServiceClient _serviceClient;
    private readonly ILogger<StorageService> _logger;

    public StorageService(
        BlobServiceClient serviceClient,
        ILogger<StorageService> logger)
    {
        _logger = logger;
        _serviceClient = serviceClient;
    }

    public async Task<string?> UploadAsync(
        Person person,
        string containerName,
        CancellationToken cancellationToken)
    {
        if (person is null)
        {
            return null;
        }
        var containerClient = _serviceClient.GetBlobContainerClient(containerName);

        var blobName = $"{person.Name}-{person.DepartmentId}";

        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        // Get a reference to a blob named in a container
        var blobClient = containerClient.GetBlobClient(blobName);

        // Upload data
        var json = JsonConvert.SerializeObject(person);
        var streamPerson = new MemoryStream(Encoding.UTF8.GetBytes(json))
        {
            Position = 0
        };

        await blobClient.UploadAsync(streamPerson, overwrite: true, cancellationToken);

        return blobClient.Uri.ToString();
    }

    public async Task<Person?> DownloadToStreamAsync(
        string blobName,
        string containerName,
        string localFilePath,
        CancellationToken cancellationToken)
    {
        var containerClient = _serviceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        try
        {
            using var stream = await blobClient.OpenReadAsync(0, null, null, cancellationToken);
            var fileStream = File.OpenWrite(localFilePath);
            await stream.CopyToAsync(fileStream, cancellationToken);
            fileStream.Close();

            Person? person = null;

            using (StreamReader file = File.OpenText(localFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                person = (Person)serializer.Deserialize(file, typeof(Person));
            }

            return person;
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, $"Directory not found: {ex.Message}");
            throw;
        }
        catch(Exception ex)
        {
            var m = 1;
            throw;
        }

    }
}