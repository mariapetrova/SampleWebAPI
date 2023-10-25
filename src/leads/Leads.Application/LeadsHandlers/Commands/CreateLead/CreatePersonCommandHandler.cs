using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Leads.Application.LeadsHandlers.Commands.CreateLead;
public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreatePersonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
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

        _context.Persons.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}