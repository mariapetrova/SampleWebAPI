using Functions.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;

namespace Functions.StartupExtensions;
public static class ConfigureFunctionsWorkerDefaults
{
    /// <summary>
    /// Adds the <see cref="FunctionExceptionMiddleware"/> to the specified <see cref="IFunctionsWorkerApplicationBuilder"/>.
    /// </summary>
    /// <param name="worker">The <see cref="IFunctionsWorkerApplicationBuilder"/>.</param>
    /// <returns>A reference to <paramref name="worker"/> after the operation has completed.</returns>
    public static IFunctionsWorkerApplicationBuilder UseFunctionExceptionMiddleware(
        this IFunctionsWorkerApplicationBuilder worker)
    {
        return worker is null ? throw new ArgumentNullException(nameof(worker)) : worker.UseMiddleware<FunctionExceptionMiddleware>();
    }
}
