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
using Domain.Entities;

namespace Leads.Endpoints;
public class UpdatePerson
{
    private readonly ISender _sender;

    public UpdatePerson(
        ISender sender)
    {
        _sender = sender;
    }

    [OpenApiOperation(operationId: OpenApiConstants.PutOperationId, Summary = FunctionConstants.UpdatePersonSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(name: OpenApiConstants.ParameterNamePersonId, In = ParameterLocation.Path, Required = true, Type = typeof(int), Summary = FunctionConstants.UpdatePersonParameterSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody(contentType: MediaTypeNames.Application.Json, bodyType: typeof(UpdatePersonCommand))]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent)]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Summary = FunctionConstants.PersonWithInvalidId, Description = FunctionConstants.PersonWithInvalidId)]
    [Function(nameof(UpdatePerson))]
    public async Task<HttpResponseData> CreateAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, FunctionConstants.HttpTriggerMethodPut, Route = Routes.Persons + Routes.PersonsPathParameter)] HttpRequestData req,
        int personId)
    {
        var updateCommand = await req.ReadFromJsonAsync<UpdatePersonCommand>();
        updateCommand.Id = personId;

        await _sender.Send(updateCommand);

        return req.CreateNoContentResponse();
    }
}