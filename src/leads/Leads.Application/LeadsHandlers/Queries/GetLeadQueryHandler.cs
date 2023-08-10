using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Leads.Application.LeadsHandlers.Queries;
public class GetLeadQueryHandler : IRequestHandler<GetLeadQuery, Lead>
{
    private readonly IApplicationDbContext _context;

    public GetLeadQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Lead> Handle(
        GetLeadQuery request,
        CancellationToken cancellationToken)
    {
        var lead = await _context.Leads.SingleOrDefaultAsync(c => c.Id == request.Id,
         cancellationToken: cancellationToken);

        return lead;
    }
}