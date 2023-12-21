using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilter
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        // bu yaptimiz islem dogrulamadir buna dikkat etacaz bii klasor uyustirb ve bir class adindan islem yapabiliriz
        //  hemda clean code yapmis oluruz controllorni sisirmiyoruz
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           var controller = context.RouteData.Values["controller"];
             var action = context.RouteData.Values["action"];

            // dto yakalamak

            var param = context.ActionArguments.SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value;

            if (param is null) 
            {
                context.Result = new BadRequestObjectResult($"Object is null " + $"Controller :{controller}" + $"Action:{action}");
                return; //400 hatasi verir
            }
            if (!context.ModelState.IsValid)
                context.Result = new UnprocessableEntityObjectResult(context.ModelState); //422 hatasi

        }
    }
}
