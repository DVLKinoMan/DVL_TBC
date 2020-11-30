using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DVL_TBC.PersonsApi.Filters
{
    //todo
    public class PersonActionFilter : IActionFilter
    {
        //private readonly ILogger _logger;
        //public PersonActionFilter(ILogger logger)
        //{
        //    _logger = logger;
        //}

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
