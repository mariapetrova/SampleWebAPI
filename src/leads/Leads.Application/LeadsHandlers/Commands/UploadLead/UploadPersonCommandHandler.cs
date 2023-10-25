using Application.Common.Interfaces;
using Application.Common.StorageBlobHelpers;
using Domain.Entities;
using MediatR;

namespace Leads.Application.LeadsHandlers.Commands.UploadLead;
public class UploadPersonCommandHandler : IRequestHandler<UploadPersonCommand, string?>
{
    private readonly IStorageService _storageService;

    public UploadPersonCommandHandler(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<string?> Handle(UploadPersonCommand request, CancellationToken cancellationToken)
    {
        var entity = new Person
        {
            DepartmentId = request.DepartmentId,
            PINCode = request.PINCode,
            Name = request.Name,
            Address = request.Address,
            MobileNumber = request.MobileNumber,
            EmailAddress = request.EmailAddress,
            Salary = request.Salary,
        };

       return await _storageService.UploadAsync(entity, BlobContainers.PersonsContainer, cancellationToken);
    }
}
