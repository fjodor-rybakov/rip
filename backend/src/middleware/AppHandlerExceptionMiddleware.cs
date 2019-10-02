using backend.helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace backend.middleware
{
    public class AppHandlerExceptionMiddleware
    {
        public static void AppHandlerException(IApplicationBuilder errorApp, ApiErrors apiErrors)
        {
            errorApp.Run(async context =>
            {
                var error = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                if (error.GetType() == typeof(Error))
                {
                    var errInstance = (Error) error;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = errInstance.HttpStatus;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errInstance));
                }
                else
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = apiErrors.ServerError.HttpStatus;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(apiErrors.ServerError));
                }
            });
        }
    }
}