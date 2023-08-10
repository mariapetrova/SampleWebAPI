using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using Functions.Extensions;

namespace Functions.Middlewares;

public class FunctionExceptionMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<FunctionExceptionMiddleware> _logger;

    public FunctionExceptionMiddleware(ILogger<FunctionExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        ProblemDetails details;
        HttpStatusCode statusCode;
        try
        
        {
            await next(context);
            return;
        }

        // currently isolated (worker) functions are throwing AggregateException
        // this behavior will be changed in a feature release so that the real exception is thrown instead of AggregateException
        catch (AggregateException ex)
        when (ex.InnerException is NotFoundValidationException valException)
        {
            statusCode = HttpStatusCode.NotFound;
            details = new ValidationProblemDetails(valException.Errors)
            {
                Status = StatusCodes.Status404NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };
        }
        catch (AggregateException ex)
        when (ex.InnerException is ValidationException valException)
        {
            statusCode = HttpStatusCode.BadRequest;
            details = new ValidationProblemDetails(valException.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
        }
        catch (AggregateException ex)
            when (ex.InnerException is JsonReaderException jsonReaderException)
        {
            statusCode = HttpStatusCode.BadRequest;
            details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = jsonReaderException.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
        }
        catch (AggregateException ex)
        when (ex.InnerException is NotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            details = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "The specified resource was not found.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };
        }
        catch (AggregateException ex)
        when (ex.InnerException is FormatException formatException)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "Input format error", new string[] { formatException.Message } }
            };
            statusCode = HttpStatusCode.BadRequest;
            details = new ValidationProblemDetails(errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
        }
        catch (FunctionInputConverterException ex)
        {
            var trimIndex = ex.Message.IndexOf("Error:Newtonsoft", StringComparison.OrdinalIgnoreCase);
            statusCode = HttpStatusCode.BadRequest;
            details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = ex.Message[..trimIndex],
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Internal Server Error");

            statusCode = HttpStatusCode.InternalServerError;
            details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.1"
            };
        }

        await context.SetHttpResponseStatusCodeAsync(statusCode, details);
    }
}