using System;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Verbatims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Misc;
using Services.Abstractions;
using TitsAPI.Controllers;

namespace TitsAPI.Filters
{
    public class ManagerTokenFilter : IAsyncActionFilter
    {
        private ITokenSessionService _tokenSessionService;
        
        public ManagerTokenFilter(ITokenSessionService tokenSessionService)
        {
            _tokenSessionService = tokenSessionService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.TryGetAuthToken(out var authToken))
            {
                var managerTokenSession = await _tokenSessionService.GetManagerSessionByToken(authToken);
                if (managerTokenSession == null)
                {
                    context.Result = new UnauthorizedObjectResult(new ErrorDto(MessagesVerbatim.AuthTokenUnknown));
                }
                else
                {
                    if (managerTokenSession.EndDate > DateTime.Now)
                    {
                        ((TitsController)context.Controller).ManagerTokenSession = managerTokenSession;
                        await next.Invoke();
                    }
                    else
                    {
                        context.Result = new UnauthorizedObjectResult(new ErrorDto(MessagesVerbatim.AuthTokenExpired));
                    }
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult(new ErrorDto(MessagesVerbatim.AuthTokenMissing));
            }
        }
    }
}