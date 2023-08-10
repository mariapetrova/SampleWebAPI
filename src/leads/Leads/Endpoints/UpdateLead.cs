using Functions.Common;
using Leads.Application.LeadsHandlers.Commands.UpdateLead;
using MediatR;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System.Net.Mime;
using System.Net;
using Leads.Common;
using Functions.Extensions;
using OpenApiConstants = Functions.Common.OpenApiConstants;

namespace Leads.Endpoints;
public class UpdateLead
{
    private readonly ISender _sender;

    public UpdateLead(
        ISender sender)
    {
        _sender = sender;
    }

    [OpenApiOperation(operationId: OpenApiConstants.PutOperationId, Summary = FunctionConstants.UpdateLeadSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: OpenApiConstants.ParameterNameLeadId, In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = FunctionConstants.UpdateLeadParameterSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody(contentType: MediaTypeNames.Application.Json, bodyType: typeof(UpdateLeadCommand))]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent)]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = FunctionConstants.LeadWithInvalidId, Description = FunctionConstants.LeadWithInvalidId)]
    [Function(nameof(UpdateLead))]
    public async Task<HttpResponseData> CreateAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, FunctionConstants.HttpTriggerMethodPut, Route = Routes.Leads + Routes.LeadsPathParameter)] HttpRequestData req,
        int leadId)
    {
        var updateCommand = await req.ReadFromJsonAsync<UpdateLeadCommand>();
        updateCommand.Id = leadId;

        await _sender.Send(updateCommand);

        return req.CreateNoContentResponse();
    }
}