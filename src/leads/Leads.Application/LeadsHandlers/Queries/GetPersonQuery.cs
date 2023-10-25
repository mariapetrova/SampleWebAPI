using Domain.Entities;
using MediatR;

namespace Leads.Application.LeadsHandlers.Queries;

public record GetPersonQuery() : IRequest<Person>
{
    public int? Id { get; set; }
}