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
    public class CourierTokenFilter : IAsyncActionFilter
    {
        private ITokenSessionService _tokenSessionService;

        public CourierTokenFilter(ITokenSessionService tokenSessionService)
        {
            _tokenSessionService = tokenSessionService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.TryGetAuthToken(out var authToken))
            {
                var courierTokenSession = await _tokenSessionService.GetCourierSessionByToken(authToken);
                if (courierTokenSession == null)
                {
                    context.Result = new UnauthorizedObjectResult(new ErrorDto(MessagesVerbatim.AuthTokenUnknown));
                }
                else
                {
                    if (courierTokenSession.EndDate > DateTime.Now)
                    {
                        ((TitsController)context.Controller).CourierTokenSession = courierTokenSession;
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