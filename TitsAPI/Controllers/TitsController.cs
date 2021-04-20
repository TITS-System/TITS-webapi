using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models.Db.TokenSessions;
using Models.DTOs.Misc;
using Services.Abstractions;

namespace TitsAPI.Controllers
{
    // TODO: Remove this Route Attribute, it is only required for Swagger
    [Route("/api/[controller]/[action]/")]
    public class TitsController : Controller
    {
        private ITokenSessionService _tokenSessionService;

        public CourierTokenSession CourierTokenSession;
        
        public ManagerTokenSession ManagerTokenSession;

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
    }
}