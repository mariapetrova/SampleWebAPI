using Functions.Common;
using Leads.Application.LeadsHandlers.Queries;
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
using Domain.Entities;

namespace Leads.Endpoints;
public class GetLead
{
    private readonly ISender _sender;

    public GetLead(
        ISender sender)
    {
        _sender = sender;
    }

    [OpenApiOperation(operationId: OpenApiConstants.GetOperationId, Summary = FunctionConstants.GetLeadSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: OpenApiConstants.ParameterNameLeadId, In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = FunctionConstants.LeadParameterLeadSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(Lead), Summary = FunctionConstants.GetLeadResponse, Description = FunctionConstants.GetLeadResponse)]
    [Function(nameof(GetLead))]
    public async Task<HttpResponseData> GetAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, FunctionConstants.HttpTriggerMethodGet, Route = Routes.Leads + Routes.LeadsPathParameter)] HttpRequestData req,
        int leadId)
    {
        var result = await _sender.Send(new GetLeadQuery
        {
            Id = leadId
        });
        return req.CreateOkResponse(result);
    }
}