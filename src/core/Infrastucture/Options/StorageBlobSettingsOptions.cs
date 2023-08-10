namespace Infrastructure.Options;
public class StorageBlobSettingsOptions
{
    public const string StorageBlobSettings = "AzureStorageBlob";

    public string Container { get; set; }

    public string ContainerName => $"{Container}";
}