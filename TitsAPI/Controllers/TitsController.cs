using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models.DTOs.Misc;

namespace TitsAPI.Controllers
{
    public class TitsController : Controller
    {
        protected TitsDbContext Context;

        public TitsController(TitsDbContext context)
        {
            Context = context;
        }

        [NonAction]
        public ActionResult TitsError(string error)
        {
            return BadRequest(new ErrorDto(error));
        }

        [NonAction]
        public ActionResult TitsMessage(string message)
        {
            return Ok(new MessageDto(message));
        }

        [NonAction]
        protected async Task<AccountSession> GetRequestSession()
        {
            var headers = ControllerContext.HttpContext.Request.Headers;
            if (headers.ContainsKey("auth-token"))
            {
                string authToken = headers["auth-token"];

                var accountSession = await Context
                    .AccountSessions
                    .Include(session => session.Account)
                    .FirstOrDefaultAsync(session => session.Token == authToken);

                if (accountSession != null)
                {
                    return accountSession;
                }
                else
                {
                    throw new ArgumentException($"{nameof(GetRequestSession)}() Was Called With No Session");
                }
            }
            else
            {
                throw new ArgumentException($"{nameof(GetRequestSession)}() Was Called With No auth-token Passed");
            }
        }
    }
}