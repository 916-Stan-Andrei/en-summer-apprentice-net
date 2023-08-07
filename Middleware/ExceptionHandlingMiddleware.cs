using System.Net;
using System.Text.Json;
using TMS.Exceptions;

namespace TMS.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _nextRequestDelegate;

        public ExceptionHandlingMiddleware(RequestDelegate nextRequestDelegate)
        {
            _nextRequestDelegate = nextRequestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _nextRequestDelegate(httpContext);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            string message;
            switch (ex)
            {
                case ArgumentNullException e:
                    {
                        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        message = ex.Message;
                        break;
                    }
                case EntityNotFoundException e:
                    {
                        httpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                        message = ex.Message;
                        break;
                    }
                default:
                    {
                        if (ex.Message.Contains("Invalid Token"))
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            message = ex.Message;
                            break;
                        }
                        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message = "Internal server error!";
                        break;
                    }
            }
            var result = JsonSerializer.Serialize(new { errorMessage = message });
            await httpContext.Response.WriteAsync(result);
        }
    }
}
