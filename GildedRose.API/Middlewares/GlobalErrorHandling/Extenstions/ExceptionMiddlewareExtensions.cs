using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace GildedRose.API.Middlewares.GlobalErrorHandling.Extenstions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
       
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.StatusCode = contextFeature.Error.GetStatusCode();

                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            ErrorCode = contextFeature.Error.GetStatusCode(),
                            Message = contextFeature.Error.Message.Trim(), 
                        }.ToString());
                    }
                });
            });
        }
    }
}
