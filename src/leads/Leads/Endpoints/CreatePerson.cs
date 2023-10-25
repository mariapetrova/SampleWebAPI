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
using System.Linq.Expressions;

namespace Leads.Endpoints;
public class CreatePerson
{
    private readonly ISender _sender;

    public CreatePerson(ISender sender)
    {
        _sender = sender;
    }

    [OpenApiOperation(operationId: OpenApiConstants.CreateOperationId, Summary = FunctionConstants.CreatePersonSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody(contentType: MediaTypeNames.Application.Json, bodyType: typeof(CreatePersonCommand))]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Created)]
    [Function(nameof(CreatePerson))]
    public async Task<HttpResponseData> CreateAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, FunctionConstants.HttpTriggerMethodPost, Route = Routes.Persons)] HttpRequestData req)
    {
        var id = 0;
        try
        {
            var createCommand = await req.ReadFromJsonAsync<CreatePersonCommand>();
            id = await _sender.Send(createCommand);
        }
        catch(Exception ex){
           var m = 1;
        }

        return req.CreateCreatedResponse($"{Routes.Persons}/{id}");
    }
}