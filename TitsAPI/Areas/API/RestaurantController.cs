using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Abstractions;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class RestaurantController : TitsController
    {
        private IRestaurantService _restaurantService;
        public RestaurantController(ITokenSessionService tokenSessionService, IRestaurantService restaurantService) : base(tokenSessionService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<RestaurantDto>> GetInfo(long restaurantId)
        {
            var tokenSession = await GetRequestSession();

            try
            {
                var restaurantDto = await _restaurantService.GetInfo(restaurantId);
                return restaurantDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}