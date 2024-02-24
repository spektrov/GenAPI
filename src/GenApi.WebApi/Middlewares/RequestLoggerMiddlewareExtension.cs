using System.Diagnostics;

namespace GenApi.WebApi.Middlewares;

public class RequestLoggerMiddlewareExtension
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggerMiddlewareExtension> _logger;

    public RequestLoggerMiddlewareExtension(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<RequestLoggerMiddlewareExtension>();
    }

    public async Task Invoke(HttpContext context)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();

        var method = context.Request.Method;
        var path = context.Request.Path;
        var queryParameters = context.Request.Query.Count > 0
            ? context.Request.QueryString.ToString()
            : "none";
        var statusCode = context.Response.StatusCode.ToString();

        await _next(context);

        _logger.LogInformation(
            "Requested endpoint: {Method} {Endpoint}\nWith parameters {Parameters}\nStatus code: {StatusCode}\nExecution time: {Time}",
            method,
            path,
            queryParameters,
            statusCode,
            stopwatch.Elapsed.ToString());
    }
}
