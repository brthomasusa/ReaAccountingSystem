using Microsoft.AspNetCore.Diagnostics;
using ReaAccountingSys.Server.ErrorModel;
using System.Net;

namespace ReaAccountingSys.Server.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILogger<WebApplication> logger)
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