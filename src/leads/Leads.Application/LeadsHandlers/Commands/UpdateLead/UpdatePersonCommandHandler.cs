using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Leads.Application.LeadsHandlers.Commands.UpdateLead;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdatePersonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Persons
            .SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException(nameof(Person), request.Id);

        entity.Name = request.Name;
        entity.PINCode = request.PINCode;
        entity.Address = request.Address;
        entity.MobileNumber = request.MobileNumber;
        entity.EmailAddress = request.EmailAddress;
        entity.Salary = request.Salary;

        _context.Persons.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}