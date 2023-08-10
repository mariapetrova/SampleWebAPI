using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Leads.Application.LeadsHandlers.Commands.UpdateLead;

public class UpdateLeadCommandHandler : IRequestHandler<UpdateLeadCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateLeadCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateLeadCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Leads
            .SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException(nameof(Lead), request.Id);

        entity.Name = request.Name;
        entity.PINCode = request.PINCode;
        entity.Address = request.Address;
        entity.MobileNumber = request.MobileNumber;
        entity.EmailAddress = request.EmailAddress;

        _context.Leads.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}