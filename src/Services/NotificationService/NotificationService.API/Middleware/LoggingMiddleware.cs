using System.Text;
using NotificationService.Application.Enums;
using NotificationService.API.Helpers;
using Newtonsoft.Json;


namespace NotificationService.API.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        Guid.TryParse(httpContext.Request.Headers["correlationId"], out Guid correlationId);

        if (correlationId == Guid.Empty)
        {
            correlationId = Guid.NewGuid();
            httpContext.Request.Headers["correlationId"] = correlationId.ToString();
        }

        DateTime logDate = DateTime.Now;

        HttpResponse httpResponse = httpContext.Response;
        httpResponse.Headers.Add("correlationId", correlationId.ToString());
        Stream originalResponseBody = httpResponse.Body;

        try
        {
            using var newResponseBody = new MemoryStream();
            httpResponse.Body = newResponseBody;

            await WriteLogAsync(httpContext, correlationId, LogLevel.Information, LogType.Request.ToString(), await ReadBodyFromRequest(httpContext.Request), logDate);

            await _next(httpContext);

            newResponseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(httpResponse.Body).ReadToEndAsync();

            newResponseBody.Seek(0, SeekOrigin.Begin);
            await newResponseBody.CopyToAsync(originalResponseBody);

            await WriteLogAsync(httpContext, correlationId, LogLevel.Information, LogType.Response, responseBodyText, logDate);

        }
        catch (Exception exp)
        {
            await WriteLogAsync(httpContext, correlationId, LogLevel.Error, LogType.Response, GetExceptionDetailString(exp), logDate);
        }
    }

    private async Task WriteLogAsync(HttpContext httpContext, Guid correlationId, LogLevel logLevel, string logType, string message, DateTime logDate)
    {
        if (httpContext.Request.Path != "/")
        {
            var requestLogMessage = new
            {
                CorrelationId = correlationId,
                Caller = httpContext.Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                LogLevel = logLevel,
                LogType = logType,
                Message = message,
                UrlHost = httpContext.Request.Host.ToString(),
                UriPath = httpContext.Request.Path,
                UrlQueryString = httpContext.Request.QueryString.ToString(),
                Headers = MiddlewareHelper.FormatHeaders(httpContext.Request.Headers),
                UrlMethod = httpContext.Request.Method,
                ResponseStatusCode = httpContext?.Response?.StatusCode ?? null,
                LogDate = logDate,
                ProcessCost = (DateTime.Now - logDate).TotalMilliseconds

            };

            _logger.Log(logLevel, "", requestLogMessage);
        }
    }

    private static async Task<string> ReadBodyFromRequest(HttpRequest request)
    {
        request.EnableBuffering();

        using var streamReader = new StreamReader(request.Body, leaveOpen: true);
        var requestBody = await streamReader.ReadToEndAsync();

        var serializeBody = JsonConvert.SerializeObject(new
        {
            Body = requestBody,
            Headers = JsonConvert.SerializeObject(request.Headers)
        });

        request.Body.Position = 0;
        return serializeBody;
    }

    private string GetExceptionDetailString(Exception exp)
    {
        StringBuilder exceptionDetail = new StringBuilder();
        exceptionDetail.Append($"Exception Type: {exp.GetType().Name} {Environment.NewLine}");
        exceptionDetail.Append($"Exception Message: {exp.Message} {Environment.NewLine}");
        exceptionDetail.Append($"Exception Stack Trace: {exp.StackTrace} {Environment.NewLine}");

        if (exp.InnerException != null)
        {
            exceptionDetail.Append($"Inner Exception Type: {exp.InnerException.GetType().Name}  {Environment.NewLine}");
            exceptionDetail.Append($"Inner Exception Message: {exp.InnerException.Message}  {Environment.NewLine}");
            exceptionDetail.Append($"Inner Exception Stack Trace: {exp.InnerException.StackTrace}  {Environment.NewLine}");
        }

        return exceptionDetail.ToString();
    }
}