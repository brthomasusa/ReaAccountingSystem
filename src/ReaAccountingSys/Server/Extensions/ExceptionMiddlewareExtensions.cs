using Microsoft.AspNetCore.Diagnostics;
using LoggingService.Interfaces;
using Server.ErrorModel;
using System.Net;

namespace Server.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature is not null)
                    {
                        logger.LogError($"Problem detected: {contextFeature.Error}");
                        await context.Response.WriteAsync
                        (
                            new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal server error",
                            }.ToString()
                        );
                    }
                });
            });
        }
    }
}