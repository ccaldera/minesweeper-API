using CC.Minesweeper.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CC.Minesweeper.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
        {
            HttpStatusCode code;

            var message = ex.Message;

            if (ex is ResourceNotFoundException)
            {
                code = HttpStatusCode.NotFound;

                logger.LogInformation(message);
            }
            else if (ex is BusinessException)
            {
                code = HttpStatusCode.Conflict;

                logger.LogInformation(message);
            }
            else
            {
                code = HttpStatusCode.InternalServerError;

                logger.LogCritical(ex, message);
            }

            var result = JsonConvert.SerializeObject(new { error = message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
