using System.Net;
using StockService.Common.Models;
using Newtonsoft.Json;
using System.Text;
using StockService.Common.Enums;
using StockService.Common.Exceptions;
namespace StockService.API.Middleware;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exp)
        {
            await HandleExceptionAsync(context, exp);
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.Clear();
        context.Response.ContentType = "application/json; charset=utf-8";
        context.Response.StatusCode = GetStatusCode(exception);

        BaseResponseModel response = new BaseResponseModel
        {
            IsSuccess = false,
            Message = GetMessage(exception),
            Code = GetErrorCode(exception)
        };

        string responseJson = JsonConvert.SerializeObject(response, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        });

        Guid.TryParse(context.Request.Headers["correlationId"], out Guid correlationId);

        await WriteLogAsync(context, correlationId, LogLevel.Error, LogType.Response, GetExceptionDetailString(exception), DateTime.Now);
        await context.Response.WriteAsync(responseJson);

    }

    private string GetExceptionDetailString(Exception exp)
    {
        StringBuilder exceptionDetail = new StringBuilder();
        exceptionDetail.Append($"Exception Type: {exp.GetType().Name} {Environment.NewLine}");
        exceptionDetail.Append($"Exception Message: {exp.Message} {Environment.NewLine}");
        exceptionDetail.Append($"Exception Stack Trace: {exp.StackTrace} {Environment.NewLine}");

        if (exp.InnerException != null)
        {
            exceptionDetail.Append($"Inner Exception Type: {exp.InnerException.GetType().Name} {Environment.NewLine}");
            exceptionDetail.Append($"Inner Exception Message: {exp.InnerException.Message} {Environment.NewLine}");
            exceptionDetail.Append($"Inner Exception Stack Trace: {exp.InnerException.StackTrace} {Environment.NewLine}");
        }

        return exceptionDetail.ToString();
    }

    public static Dictionary<string, string> FormatHeaders(IHeaderDictionary headers)
    {
        Dictionary<string, string> returnValue = new Dictionary<string, string>();

        if (headers != null)
        {
            foreach (var header in headers.Where(x => !string.IsNullOrEmpty(x.Key) || !string.IsNullOrEmpty(x.Value)))
            {
                if (!headers.Keys.Any(x => x == header.Key))
                {
                    headers.Add(header.Key, header.Value);
                }
            }
        }

        return returnValue;
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
                Headers = FormatHeaders(httpContext.Request.Headers),
                UrlMethod = httpContext.Request.Method,
                ResponseStatusCode = httpContext?.Response?.StatusCode ?? null,
                LogDate = logDate,
                ProcessCost = (DateTime.Now - logDate).TotalMilliseconds

            };

            _logger.Log(logLevel, "", requestLogMessage);
        }
    }

    private int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            DBOperationException => (int)HttpStatusCode.InternalServerError,
            StockException => (int)HttpStatusCode.InternalServerError,
            StockNotfoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };
    }

    private string GetMessage(Exception exception)
    {
        return exception switch
        {
            DBOperationException => ErrorMessages.DB_ERROR_MESSAGE,
            StockException => ErrorMessages.STOCK_EXCEPTION_MESSAGE,
            StockNotfoundException => ErrorMessages.STOCK_NOT_FOUND_MESSAGE,
            _ => ErrorMessages.DefaultException
        };
    }

    private string GetErrorCode(Exception exception)
    {
        return exception switch
        {
            DBOperationException => ErrorCodes.DB_ERROR_CODE,
            StockException => ErrorCodes.STOCK_ERROR_CODE,
            StockNotfoundException => ErrorCodes.STOCK_NOT_FOUND_CODE,
            _ => ErrorCodes.DefaultException
        };
    }

}