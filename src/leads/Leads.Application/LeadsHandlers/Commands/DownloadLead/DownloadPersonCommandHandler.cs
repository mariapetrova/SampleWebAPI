using Application.Common.Interfaces;
using Application.Common.StorageBlobHelpers;
using Domain.Entities;
using MediatR;

namespace Leads.Application.LeadsHandlers.Commands.DownloadLead;

public class DownloadPersonCommandHandler : IRequestHandler<DownloadPersonCommand, Person?>
{
    private readonly IStorageService _storageService;

    public DownloadPersonCommandHandler(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<Person?> Handle(
        DownloadPersonCommand request,
        CancellationToken cancellationToken)
    {
        var blobName = $"{request.Name}-{request.DepartmentId}";
        var localFilePath = "c:\\home\\RequestContent.json";
        return await _storageService.DownloadToStreamAsync(
            blobName, BlobContainers.PersonsContainer, localFilePath, cancellationToken);
    }
}
