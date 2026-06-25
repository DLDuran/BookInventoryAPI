using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace BookInventory.Api.Controllers;

public class ApiControllerBase : ControllerBase, IActionFilter
{
    protected long CurrentUserId
    {
        get
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return long.TryParse(claim, out long id) ? id : 0;
        }
    }

    [NonAction]
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (CurrentUserId == 0)
        {
            context.Result = new UnauthorizedObjectResult(
                new
                {
                    message = "System cannot identify the user."
                });
        }
    }

    [NonAction]
    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
}
