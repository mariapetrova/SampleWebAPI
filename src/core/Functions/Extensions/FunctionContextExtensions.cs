using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace Functions.Extensions;
public static class FunctionContextExtensions
{
    public static async Task SetHttpResponseStatusCodeAsync(
        this FunctionContext context,
        HttpStatusCode statusCode,
        ProblemDetails details)
    {
        var req = await context.GetHttpRequestDataAsync();

        var res = req!.CreateResponse();

        await res.WriteAsJsonAsync(details);
        res.StatusCode = statusCode;

        context.GetInvocationResult().Value = res;
    }
}