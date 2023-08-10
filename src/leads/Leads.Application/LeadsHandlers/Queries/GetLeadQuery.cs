using Domain.Entities;
using MediatR;

namespace Leads.Application.LeadsHandlers.Queries;

public record GetLeadQuery() : IRequest<Lead>
{
    public int? Id { get; set; }
}