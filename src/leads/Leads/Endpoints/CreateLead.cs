using MediatR;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Net;
using System.Net.Mime;
using Microsoft.OpenApi.Models;
using Functions.Common;
using Functions.Extensions;
using Leads.Common;
using Leads.Application.LeadsHandlers.Commands.CreateLead;
using OpenApiConstants = Functions.Common.OpenApiConstants;

namespace Leads.Endpoints;
public class CreateLead
{
    private readonly ISender _sender;

    public CreateLead(ISender sender)
    {
        _sender = sender;
    }

    [OpenApiOperation(operationId: OpenApiConstants.CreateOperationId, Summary = FunctionConstants.CreateLeadSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody(contentType: MediaTypeNames.Application.Json, bodyType: typeof(CreateLeadCommand))]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Created)]
    [Function(nameof(CreateLead))]
    public async Task<HttpResponseData> CreateAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, FunctionConstants.HttpTriggerMethodPost, Route = Routes.Leads)] HttpRequestData req)
    {
        var createCommand = await req.ReadFromJsonAsync<CreateLeadCommand>();
        var id = await _sender.Send(createCommand);

        return req.CreateCreatedResponse($"{Routes.Leads}/{id}");
    }
}