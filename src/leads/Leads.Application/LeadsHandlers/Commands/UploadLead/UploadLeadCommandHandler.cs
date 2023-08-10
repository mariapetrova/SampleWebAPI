using Application.Common.Interfaces;
using Application.Common.StorageBlobHelpers;
using Domain.Entities;
using MediatR;

namespace Leads.Application.LeadsHandlers.Commands.UploadLead;
public class UploadLeadCommandHandler : IRequestHandler<UploadLeadCommand, string?>
{
    private readonly IStorageService _storageService;

    public UploadLeadCommandHandler(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<string?> Handle(UploadLeadCommand request, CancellationToken cancellationToken)
    {
        var entity = new Lead
        {
            PINCode = request.PINCode,
            Name = request.Name,
            Address = request.Address,
            MobileNumber = request.MobileNumber,
            EmailAddress = request.EmailAddress,
        };

       return await _storageService.UploadAsync(entity, BlobContainers.LeadsContainer, cancellationToken);
    }
}
