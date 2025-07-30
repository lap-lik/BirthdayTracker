using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message) = exception switch
            {
                ArgumentException or ArgumentNullException
                    => (HttpStatusCode.BadRequest, "Invalid request data"),
                InvalidPasswordException 
                    => (HttpStatusCode.BadRequest, exception.Message),
                InvalidEmailException
                    => (HttpStatusCode.BadRequest, exception.Message),
                NotFoundException
                    => (HttpStatusCode.NotFound, exception.Message),
                ForbiddenException
                    => (HttpStatusCode.Forbidden, exception.Message),
                ConflictException
                    => (HttpStatusCode.Conflict, exception.Message),
                DbUpdateException
                    => (HttpStatusCode.Conflict, "Database operation failed"),
                _
                    => (HttpStatusCode.InternalServerError, "Internal server error")
            };

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsJsonAsync(new { error = message });
        }
    }
}