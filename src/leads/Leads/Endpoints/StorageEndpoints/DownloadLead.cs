using Functions.Common;
using MediatR;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Net.Mime;
using System.Net;
using Leads.Common;
using Functions.Extensions;
using OpenApiConstants = Functions.Common.OpenApiConstants;
using Domain.Entities;
using Microsoft.OpenApi.Models;
using Leads.Application.LeadsHandlers.Commands.DownloadLead;
using Newtonsoft.Json;

namespace Leads.Endpoints.StorageEndpoints;
public class DownloadLead
{
    private readonly ISender _sender;

    public DownloadLead(ISender sender)
    {
        _sender = sender;
    }

    [OpenApiOperation(operationId: OpenApiConstants.DownloadOperationId, Summary = FunctionConstants.DownloadLeadResponse, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: OpenApiConstants.ParameterNameName, In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = FunctionConstants.LeadParameterLeadSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: OpenApiConstants.ParameterNamePINCode, In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = FunctionConstants.LeadParameterLeadSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(Lead), Summary = FunctionConstants.DownloadLeadResponse, Description = FunctionConstants.DownloadLeadResponse)]
    [Function(nameof(DownloadLead))]
    public async Task<HttpResponseData> DownloadAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, FunctionConstants.HttpTriggerMethodGet, Route = Routes.LeadsDownload)] HttpRequestData req,
        string leadName,
        string pinCode)
    {
        var result = await _sender.Send(new DownloadLeadCommand
        {
            Name = leadName,
            PINCode = pinCode,
        });

        return req.CreateOkResponse(result);
    }
}