using Entities.LogModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilter
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public LogFilterAttribute(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation(Log("OnActionExecuting", context.RouteData));
        }


        private string? Log(string modelName,RouteData routeData)
        {
            var logDetails = new LogDetails()
            {
                ModelModel = modelName,  
                Controller = routeData.Values["controller"],
                Action = routeData.Values["Id"]
            };

            if (routeData.Values.Count >= 3)
                logDetails.Id = routeData.Values["Id"];

            return logDetails.ToString();
        }

       

        

    }
}
