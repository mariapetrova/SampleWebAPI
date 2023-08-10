using Functions.Common;
using MediatR;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Net.Mime;
using System.Net;
using Leads.Common;
using Leads.Application.LeadsHandlers.Commands.UploadLead;
using Functions.Extensions;

namespace Leads.Endpoints.StorageEndpoints;
public class UploadLead
{
    private readonly ISender _sender;

    public UploadLead(ISender sender)
    {
        _sender = sender;
    }

    [OpenApiOperation(operationId: OpenApiConstants.UploadOperationId, Summary = FunctionConstants.UploadLeadSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody(contentType: MediaTypeNames.Application.Json, bodyType: typeof(UploadLeadCommand))]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK)]
    [Function(nameof(UploadLead))]
    public async Task<HttpResponseData> UploadAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, FunctionConstants.HttpTriggerMethodPost, Route = Routes.LeadsUpload)] HttpRequestData req)
    {
        var uploadCommand = await req.ReadFromJsonAsync<UploadLeadCommand>();
        var url = await _sender.Send(uploadCommand);

        return req.CreateOkResponse($"{Routes.Leads}/{url}");
    }
}