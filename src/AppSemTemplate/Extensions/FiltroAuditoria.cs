using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AppSemTemplate.Extensions
{
    public class FiltroAuditoria : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var message = context.HttpContext.User.Identity.Name + " Acessou " + context.HttpContext.Request.GetDisplayUrl() + " em " + DateTime.Now;
                Console.WriteLine(message);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}
