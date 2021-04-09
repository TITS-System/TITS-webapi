using System;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Verbatims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Misc;

namespace TitsAPI.Filters
{
    public class CheckAuthTokenFilter : IAsyncActionFilter
    {
        private readonly TitsDbContext _context;

        public CheckAuthTokenFilter(TitsDbContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.TryGetAuthToken(out var authToken))
            {
                var accountSession = await _context.AccountSessions.FirstOrDefaultAsync(session => session.Token == authToken);
                if (accountSession == null)
                {
                    context.Result = new UnauthorizedObjectResult(new ErrorDto(MessagesVerbatim.AuthTokenUnknown));
                }
                else
                {
                    if (accountSession.EndDate > DateTime.Now)
                    {
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