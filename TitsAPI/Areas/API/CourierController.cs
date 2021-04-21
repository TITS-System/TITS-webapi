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
    public class CourierController : TitsController
    {
        private ICourierAccountService _courierAccountService;
        private IAccountRoleService _accountRoleService;
        private ITokenSessionService _tokenSessionService;
        private IRestaurantService _restaurantService;

        public CourierController(ITokenSessionService tokenSessionService, ICourierAccountService courierAccountService, IAccountRoleService accountRoleService, IRestaurantService restaurantService) : base(tokenSessionService)
        {
            _tokenSessionService = tokenSessionService;
            _courierAccountService = courierAccountService;
            _accountRoleService = accountRoleService;
            _restaurantService = restaurantService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResultDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var loginResultDto = await _tokenSessionService.LoginCourier(loginDto);

                return loginResultDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<CreatedDto>> CreateCourier([FromBody] CreateCourierAccountDto createCourierAccountDto)
        {
            try
            {
                var createdDto = await _courierAccountService.CreateCourier(createCourierAccountDto);

                await _accountRoleService.AddToRole(createdDto.Id, WorkerRolesVerbatim.Courier);

                await _courierAccountService.AssignToRestaurant(
                    new()
                    {
                        CourierId = createdDto.Id,
                        RestaurantId = createCourierAccountDto.RestaurantId
                    });

                return createdDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CourierTokenFilter))]
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
        public async Task<ActionResult<CourierFullInfoDto>> GetFullInfo(long courierId)
        {
            try
            {
                var courierFullInfoDto = await _courierAccountService.GetFullInfo(courierId);
                return courierFullInfoDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
        
        [HttpPost]
        [TypeFilter(typeof(CourierTokenFilter))]
        public async Task<ActionResult> ChangeProfile([FromBody] ChangeCourierProfileDto changeCourierProfileDto)
        {
            try
            {
                await _courierAccountService.ChangeCourierProfile(changeCourierProfileDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
        
        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<GetCouriersResultDto>> GetAllByRestaurant(long restaurantId)
        {
            try
            {
                var getCouriersResultDto = await _restaurantService.GetCouriers(restaurantId);
                return getCouriersResultDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}