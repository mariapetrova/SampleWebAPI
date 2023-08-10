using MediatR;
using System.Text.Json.Serialization;

namespace Leads.Application.LeadsHandlers.Commands.UploadLead;
public class UploadLeadCommand : IRequest<string?>
{
    [JsonIgnore]
    public string PINCode { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string MobileNumber { get; set; }

    public string EmailAddress { get; set; }
}