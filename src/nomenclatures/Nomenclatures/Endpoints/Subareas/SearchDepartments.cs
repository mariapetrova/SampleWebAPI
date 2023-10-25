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
public class SearchDepartments
{
    private readonly ISender _sender;

    public SearchDepartments(
        ISender sender)
    {
        _sender = sender;
    }

    //[OpenApiSecurity(OpenApiConstants.SchemeName, SecuritySchemeType.OAuth2, Flows = typeof(FunctionOpenApiOAuthFlow))]
    [OpenApiOperation(operationId: OpenApiConstants.GetOperationId + Routes.Departments, tags: Routes.Departments, Summary = FunctionConstants.SearchDepartmentsSummary, Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: MediaTypeNames.Application.Json, bodyType: typeof(List<Department>), Summary = FunctionConstants.SearchDepartmentsResponse, Description = FunctionConstants.SearchDepartmentsResponse)]
    [OpenApiParameter(name: OpenApiConstants.ParameterNameDepartmentId, In = ParameterLocation.Query, Required = false, Type = typeof(int), Summary = FunctionConstants.DepartmentsParameterIdSummary, Visibility = OpenApiVisibilityType.Important)]
    [Function(nameof(SearchDepartments))]
    public async Task<HttpResponseData> GetAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, FunctionConstants.HttpTriggerMethodGet, Route = Routes.Departments)] HttpRequestData req,
        int departmentId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new SearchDepartmentsQuery()
            {
                Id = departmentId
            },
            cancellationToken);

        return req.CreateOkResponse(result);
    }
}
