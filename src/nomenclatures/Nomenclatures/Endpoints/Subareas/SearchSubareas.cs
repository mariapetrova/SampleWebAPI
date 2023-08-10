using MediatR;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Nomenclatures.Application.SubareasHandlers.Queries.SearchSubareas;
using Nomenclatures.Common;
using System.Net;
using System.Net.Mime;
using Microsoft.OpenApi.Models;
using OpenApiConstants = Functions.Common.OpenApiConstants;
using Functions.Common;
using Functions.Extensions;
using Domain.Entities;

namespace Nomenclatures.Endpoints.Subareas;
public class SearchSubareas
{
    private readonly ISender _sender;

    public SearchSubareas(
        ISender sender)
    {
        _sender = sender;
    }

    //[OpenApiSecurity(OpenApiConstants.SchemeName, SecuritySchemeType.OAuth2, Flows = typeof(FunctionOpenApiOAuthFlow))]
    [OpenApiOperation(operationId: OpenApiConstants.GetOperationId + Routes.Subareas, tags: Routes.Subareas, Summary = FunctionConstants.SearchSubareasSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(List<Subarea>), Summary = FunctionConstants.SearchSubareasResponse, Description = FunctionConstants.SearchSubareasResponse)]
    [OpenApiParameter(name: OpenApiConstants.ParameterNamePINCode, In = ParameterLocation.Query, Required = false, Type = typeof(string), Summary = FunctionConstants.SubareasParameterPINCodeSummary, Visibility = OpenApiVisibilityType.Important)]
    [Function(nameof(SearchSubareas))]
    public async Task<HttpResponseData> GetAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, FunctionConstants.HttpTriggerMethodGet, Route = Routes.Subareas)] HttpRequestData req,
        string pinCode,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new SearchSubareasQuery()
            {
                PINCode = pinCode
            },
            cancellationToken);

        return req.CreateOkResponse(result);
    }
}
