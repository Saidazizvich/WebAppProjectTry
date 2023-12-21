using Entities.ErrorModel;
using Entities.Exeptions;
using Microsoft.AspNetCore.Diagnostics;
using Services.Concreate;
using System.Net;
using System.Runtime.CompilerServices;

namespace WebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
         // burda esa middilware islemi gerceklesti ve calisti hem logger hem boska
        public static void ConfigureExeptionHandler(this WebApplication app, ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                   
                    context.Response.ContentType = "appsettings/json";


                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFound => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        }; 


                        logger.LogError($"it is not very error: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString()) ;


                    }

                });

            });
        }


    }

}     