using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models.Db.Sessions;
using Models.DTOs.Misc;
using Services.Abstractions;

namespace TitsAPI.Controllers
{
    // TODO: Remove this Route Attribute, it is only required for Swagger
    [Route("/api/[controller]/[action]/")]
    public class TitsController : Controller
    {
        private ITokenSessionService _tokenSessionService;

        public TitsController(ITokenSessionService tokenSessionService)
        {
            _tokenSessionService = tokenSessionService;
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
        protected async Task<TokenSession> GetRequestSession()
        {
            var headers = ControllerContext.HttpContext.Request.Headers;
            if (headers.ContainsKey("auth-token"))
            {
                string authToken = headers["auth-token"];

                var requestSession = await _tokenSessionService.GetByToken(authToken);

                if (requestSession != null)
                {
                    return requestSession;
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