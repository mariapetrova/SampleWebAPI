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
public class DownloadPerson
{
    private readonly ISender _sender;

    public DownloadPerson(ISender sender)
    {
        _sender = sender;
    }

    [OpenApiOperation(operationId: OpenApiConstants.DownloadOperationId, Summary = FunctionConstants.DownloadPersonResponse, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: OpenApiConstants.ParameterNamePerson, In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = FunctionConstants.PersonParameterPersonSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: OpenApiConstants.ParameterNameDepartmentId, In = ParameterLocation.Query, Required = true, Type = typeof(int), Summary = FunctionConstants.PersonParameterPersonSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(Person), Summary = FunctionConstants.DownloadPersonResponse, Description = FunctionConstants.DownloadPersonResponse)]
    [Function(nameof(DownloadPerson))]
    public async Task<HttpResponseData> DownloadAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, FunctionConstants.HttpTriggerMethodGet, Route = Routes.PersonsDownload)] HttpRequestData req,
        string personName,
        int departmentId)
    {
        var result = await _sender.Send(new DownloadPersonCommand
        {
            Name = personName,
            DepartmentId = departmentId,
        });

        return req.CreateOkResponse(result);
    }
}