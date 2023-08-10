using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace Functions.Extensions;
public static class HttpRequestDataExtensions
{
    public static HttpResponseData CreateOkResponse<TResponse>(this HttpRequestData req, TResponse obj)
    {
        var response = req.CreateResponse();

        var status = HttpStatusCode.OK;
        if (obj != null)
        {
            response.WriteAsJsonAsync(obj);
        }
        else
        {
            status = HttpStatusCode.NotFound;

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "The specified resource was not found.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };

            response.WriteAsJsonAsync(details);
        }

        response.StatusCode = status;
        return response;
    }

    public static HttpResponseData CreateCreatedResponse(this HttpRequestData req, string route)
    {
        var response = req.CreateResponse(HttpStatusCode.Created);
        response.Headers.Add(HeaderNames.Location, route);

        return response;
    }

    public static HttpResponseData CreateNoContentResponse(this HttpRequestData req)
    {
        return req.CreateResponse(HttpStatusCode.NoContent);
    }
}