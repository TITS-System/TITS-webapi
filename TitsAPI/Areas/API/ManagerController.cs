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
    public class ManagerController : TitsController
    {
        private ICourierAccountService _courierAccountService;
        private IManagerAccountService _managerAccountService;
        private IAccountRoleService _accountRoleService;
        private ITokenSessionService _tokenSessionService;
        private IRestaurantService _restaurantService;

        public ManagerController(ITokenSessionService tokenSessionService, IManagerAccountService managerAccountService, ICourierAccountService courierAccountService, IAccountRoleService accountRoleService, IRestaurantService restaurantService) : base(tokenSessionService)
        {
            _tokenSessionService = tokenSessionService;
            _managerAccountService = managerAccountService;
            _courierAccountService = courierAccountService;
            _accountRoleService = accountRoleService;
            _restaurantService = restaurantService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResultDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var loginResultDto = await _tokenSessionService.LoginManager(loginDto);

                return loginResultDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<CreatedDto>> CreateManager([FromBody] CreateCourierAccountDto createCourierAccountDto)
        {
            try
            {
                var createdDto = await _courierAccountService.CreateCourier(createCourierAccountDto);

                await _accountRoleService.AddToRole(createdDto.Id, WorkerRolesVerbatim.Manager);

                // We don't assign manager to restaurant, because he can manipulate many at once

                return createdDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<MessageDto>> Logout()
        {
            try
            {
                await _tokenSessionService.Logout(CourierTokenSession);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
        
        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<ManagerFullInfoDto>> GetInfo(long managerId)
        {
            try
            {
                var managerFullInfoDto = await _managerAccountService.GetManagerInfo(managerId);
                return managerFullInfoDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
        
        [HttpPost]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult> ChangeProfile([FromBody] ChangeManagerProfileDto changeManagerProfileDto)
        {
            try
            {
                await _managerAccountService.ChangeManagerProfile(changeManagerProfileDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}