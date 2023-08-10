using Domain.Entities;
using MediatR;

namespace Leads.Application.LeadsHandlers.Commands.DownloadLead;

public class DownloadLeadCommand : IRequest<Lead?>
{
    public string Name { get; set; }

    public string PINCode { get; set; }
}
