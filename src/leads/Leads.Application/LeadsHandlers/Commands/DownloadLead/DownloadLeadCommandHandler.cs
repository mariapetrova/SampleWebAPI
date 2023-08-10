using Application.Common.Interfaces;
using Application.Common.StorageBlobHelpers;
using Domain.Entities;
using MediatR;

namespace Leads.Application.LeadsHandlers.Commands.DownloadLead;

public class DownloadLeadCommandHandler : IRequestHandler<DownloadLeadCommand, Lead?>
{
    private readonly IStorageService _storageService;

    public DownloadLeadCommandHandler(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<Lead?> Handle(
        DownloadLeadCommand request,
        CancellationToken cancellationToken)
    {
        var blobName = $"{request.Name}-{request.PINCode}";
        var localFilePath = "c:\\home\\RequestContent.json";
        return await _storageService.DownloadToStreamAsync(
            blobName, BlobContainers.LeadsContainer, localFilePath, cancellationToken);
    }
}
