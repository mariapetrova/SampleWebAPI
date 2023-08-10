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
        Lead lead,
        string containerName,
        CancellationToken cancellationToken)
    {
        if (lead is null)
        {
            return null;
        }
        var containerClient = _serviceClient.GetBlobContainerClient(containerName);

        var blobName = $"{lead.Name}-{lead.PINCode}";

        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        // Get a reference to a blob named in a container
        var blobClient = containerClient.GetBlobClient(blobName);

        // Upload data
        var json = JsonConvert.SerializeObject(lead);
        var streamLead = new MemoryStream(Encoding.UTF8.GetBytes(json))
        {
            Position = 0
        };

        await blobClient.UploadAsync(streamLead, overwrite: true, cancellationToken);

        return blobClient.Uri.ToString();
    }

    public async Task<Lead?> DownloadToStreamAsync(
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

            Lead? lead = null;

            using (StreamReader file = File.OpenText(localFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                lead = (Lead)serializer.Deserialize(file, typeof(Lead));
            }

            return lead;
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