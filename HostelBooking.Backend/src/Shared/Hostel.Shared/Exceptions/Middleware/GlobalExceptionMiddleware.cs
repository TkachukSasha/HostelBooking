using Hostel.Shared.Types.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Hostel.Shared.Exceptions.Middleware
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                if ((exception is HostelException))
                    await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(exception.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
