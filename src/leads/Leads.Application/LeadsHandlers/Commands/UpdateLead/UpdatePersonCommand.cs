using MediatR;
using System.Text.Json.Serialization;

namespace Leads.Application.LeadsHandlers.Commands.UpdateLead;

//[Authorize(RoleTypes.ProgramManager)]
//[Authorize(Policy = PolicyTypes.Campaign)]
public class UpdatePersonCommand : IRequest<int>
{
    [JsonIgnore]
    public int? Id { get; set; }

    public int DepartmentId { get; set; }

    public string PINCode { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string MobileNumber { get; set; }

    public string EmailAddress { get; set; }

    public decimal Salary { get; set; }
}