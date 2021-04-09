using System.Threading.Tasks;
using Infrastructure.Verbatims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.DTOs.Misc;

namespace TitsAPI.Filters
{
    public class SudoFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Query.TryGetValue("sudo", out var sudo))
            {
                if (sudo =="egop")
                {
                    await next();
                }
                else
                {
                    context.Result = new BadRequestObjectResult(new ErrorDto(MessagesVerbatim.InvalidSudoKey));
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult(new ErrorDto(MessagesVerbatim.SudoAccessRequired));
            }
        }
    }
}