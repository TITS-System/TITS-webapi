using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.DTOs.Requests;
using Services.Abstractions;
using TitsAPI.Controllers;

namespace TitsAPI.Areas.API
{
    public class WorkerAccountController : TitsController
    {
        private ITokenSessionService _tokenSessionService;
        public WorkerAccountController(ITokenSessionService tokenSessionService) : base(tokenSessionService)
        {
            _tokenSessionService = tokenSessionService;
        }
        
        
        [HttpPost]
        public async Task<ActionResult<LoginResultDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var loginResultDto = await _tokenSessionService.Login(loginDto);

                return loginResultDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}