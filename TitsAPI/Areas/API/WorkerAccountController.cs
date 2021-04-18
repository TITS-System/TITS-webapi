using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.DTOs;
using Models.DTOs.Misc;
using Models.DTOs.Requests;
using Services.Abstractions;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class WorkerAccountController : TitsController
    {
        private IWorkerAccountService _workerAccountService;
        private ITokenSessionService _tokenSessionService;

        public WorkerAccountController(ITokenSessionService tokenSessionService, IWorkerAccountService workerAccountService) : base(tokenSessionService)
        {
            _tokenSessionService = tokenSessionService;
            _workerAccountService = workerAccountService;
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

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<GetRolesResultDto>> GetRoles()
        {
            var tokenSession = await GetRequestSession();
            try
            {
                return await _workerAccountService.GetRoles(tokenSession.WorkerAccountId);
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<MessageDto>> Logout()
        {
            var tokenSession = await GetRequestSession();
            try
            {
                await _tokenSessionService.Logout(tokenSession);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}