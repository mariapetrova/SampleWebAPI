using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Leads.Application.LeadsHandlers.Queries;
public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, Person>
{
    private readonly IApplicationDbContext _context;

    public GetPersonQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Person> Handle(
        GetPersonQuery request,
        CancellationToken cancellationToken)
    {
        var person = await _context.Persons.SingleOrDefaultAsync(c => c.Id == request.Id,
         cancellationToken: cancellationToken);

        return person;
    }
}