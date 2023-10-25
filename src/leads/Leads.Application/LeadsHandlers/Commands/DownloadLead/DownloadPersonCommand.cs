using Domain.Entities;
using MediatR;

namespace Leads.Application.LeadsHandlers.Commands.DownloadLead;

public class DownloadPersonCommand : IRequest<Person?>
{
    public string Name { get; set; }

    public int DepartmentId { get; set; }
}
