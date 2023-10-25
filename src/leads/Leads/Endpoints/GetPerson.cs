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
public class GetPerson
{
    private readonly ISender _sender;

    public GetPerson(
        ISender sender)
    {
        _sender = sender;
    }

    [OpenApiOperation(operationId: OpenApiConstants.GetOperationId, Summary = FunctionConstants.GetPersonSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: OpenApiConstants.ParameterNamePersonId, In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = FunctionConstants.PersonParameterPersonSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(Person), Summary = FunctionConstants.GetPersonResponse, Description = FunctionConstants.GetPersonResponse)]
    [Function(nameof(GetPerson))]
    public async Task<HttpResponseData> GetAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, FunctionConstants.HttpTriggerMethodGet, Route = Routes.Persons + Routes.PersonsPathParameter)] HttpRequestData req,
        int personId)
    {
        var result = await _sender.Send(new GetPersonQuery
        {
            Id = personId
        });
        return req.CreateOkResponse(result);
    }
}