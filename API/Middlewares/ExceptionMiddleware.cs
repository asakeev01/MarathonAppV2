using System;
using System.Net;
using MarathonApp.Models.Exceptions;
using Models.Exceptions;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, new HttpException(ex, HttpStatusCode.InternalServerError));
            }
        }

        private async ValueTask HandleExceptionAsync(HttpContext context, HttpException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.StatusCode;
            await context.Response.WriteAsync(new ErrorDatailsModel
            {
                StatusCode = context.Response.StatusCode,
                Message = exception?.ErrorMessage,
                InnerException = exception?.InnerException?.Message
            }.ToString());
        }
    }
}

