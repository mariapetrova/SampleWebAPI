using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Leads.Application.LeadsHandlers.Commands.CreateLead;
public class CreateLeadCommandHandler : IRequestHandler<CreateLeadCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateLeadCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
    {
        var entity = new Lead
        {
            PINCode = request.PINCode,
            Name = request.Name,
            Address = request.Address,
            MobileNumber = request.MobileNumber,
            EmailAddress = request.EmailAddress,
        };

        _context.Leads.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}