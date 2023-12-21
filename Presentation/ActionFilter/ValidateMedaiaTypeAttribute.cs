using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilter
{
    public class ValidateMedaiaTypeAttribute : ActionFilterAttribute
    {   // yani biz onceki dersda bir class tamomladik AddCustomMediaTypesadi sudir burda biz bir json formata ve xml formata veriyi donustudik serialize yardamida  
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var acceptHeaderPresent = context.HttpContext.Response.Headers.ContainsKey("Accept");

            if (acceptHeaderPresent) // simdi burda bizim istek varmi diye testden geciyoruz 
            {
                context.Result = new BadRequestObjectResult($"accept header is missing");
                return; // simdi burda bizim cevab var dikkat
            }

            var mediaType = context.HttpContext.Response.Headers["Accept"].FirstOrDefault();// accet yaptimiz zaman formata varmi degilmi bakiyoruz
              

            if(MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue? outMediaType)) 
            {
                 context.Result = new BadRequestObjectResult($"Media type not present " + "$""please add Accept header with required media type.")

                       
            }

            context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType); 
        }
    }
}
