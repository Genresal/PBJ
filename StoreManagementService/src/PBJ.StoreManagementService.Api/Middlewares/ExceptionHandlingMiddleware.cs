using FluentValidation;
using PBJ.StoreManagementService.Business.Exceptions;
using System.Net;
using Serilog;

namespace PBJ.StoreManagementService.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;

                response.ContentType = "application/json";

                var (status, message) = GetResponse(exception);

                response.StatusCode = (int)status;

                Log.Information($"Exception were thrown: {exception.GetType()}, with message: {exception.Message}" +
                                $" Response status code: {context.Response.StatusCode}");

                Log.Error(exception, "EXCEPTION WERE THROWN!");

                await response.WriteAsync(message);
            }
        }

        public (HttpStatusCode code, string message) GetResponse(Exception exception)
        {
            HttpStatusCode code;

            switch (exception)
            {
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;

                case AlreadyExistsException:
                    code = HttpStatusCode.Conflict;
                    break;

                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            return (code, exception.Message);
        }
    }
}
