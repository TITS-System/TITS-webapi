using System;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Verbatims;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.DTOs;
using Models.DTOs.Misc;
using Models.DTOs.Requests;
using Models.DTOs.WorkerAccountDtos;
using Services.Abstractions;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class WorkerAccountController : TitsController
    {
        private IWorkerAccountService _workerAccountService;
        private IWorkerRoleService _workerRoleService;
        private ITokenSessionService _tokenSessionService;

        public WorkerAccountController(ITokenSessionService tokenSessionService, IWorkerAccountService workerAccountService, IWorkerRoleService workerRoleService) : base(tokenSessionService)
        {
            _tokenSessionService = tokenSessionService;
            _workerAccountService = workerAccountService;
            _workerRoleService = workerRoleService;
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

        [HttpPost]
        public async Task<ActionResult<CreatedDto>> CreateCourier([FromBody] CreateWorkerAccountDto createWorkerAccountDto)
        {
            try
            {
                var createdDto = await _workerAccountService.CreateAccount(createWorkerAccountDto);

                await _workerRoleService.AddToRole(createdDto.Id, WorkerRolesVerbatim.Courier);

                await _workerAccountService.AssignToRestaurant(
                    new ()
                    {
                        WorkerId = createdDto.Id,
                        RestaurantId = createWorkerAccountDto.RestaurantId
                    });

                return createdDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreatedDto>> CreateManager([FromBody] CreateWorkerAccountDto createWorkerAccountDto)
        {
            try
            {
                var createdDto = await _workerAccountService.CreateAccount(createWorkerAccountDto);

                await _workerRoleService.AddToRole(createdDto.Id, WorkerRolesVerbatim.Manager);

                // We don't assign manager to restaurant, because he can manipulate many at once
                
                return createdDto;
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