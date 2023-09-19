using Core.Constants;
using Core.Services;
using System.Net;
using System.Text;

namespace Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService<ExceptionMiddleware> _loggerService;

        public ExceptionMiddleware(RequestDelegate next, ILoggerService<ExceptionMiddleware> loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }            
            catch (Exception exception)
            {
                int errorReference = _loggerService.LogException(exception, await GetRequestSummary(httpContext.Request));
                await HandleExceptionAsync(httpContext, errorReference, AppConstants.Error.GeneralErrorMessage, HttpStatusCode.InternalServerError);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int errorReference, string message, HttpStatusCode httpStatusCode)
        {
            return HandleExceptionAsync(context, $"{message} Error Reference: {errorReference}", httpStatusCode);
        }

        private static Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode httpStatusCode)
        {
           
            context.Response.StatusCode = (int)httpStatusCode;

            return context.Response.WriteAsync(message);
        }

        private static async Task<string> GetRequestSummary(HttpRequest request)
        {
            var summary = new StringBuilder();

            summary.Append($"Request Path : {request.Path} \n");

            summary.Append($"Request Method : {request.Method} \n");

            string requestBody = await GetRequestBody(request);

            if (!string.IsNullOrEmpty(requestBody))
            {
                summary.Append($"Request Body : {requestBody}");
            }

            return summary.ToString();
        }

        private static async Task<string> GetRequestBody(HttpRequest request)
        {
            using var reader = new StreamReader(request.Body);
            string body = await reader.ReadToEndAsync();

            return body;
        }
    }
}
