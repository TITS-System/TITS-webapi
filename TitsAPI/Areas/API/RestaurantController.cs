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
        private IZoneService _zoneService;

        public RestaurantController(ITokenSessionService tokenSessionService, IRestaurantService restaurantService, IZoneService zoneService) : base(tokenSessionService)
        {
            _restaurantService = restaurantService;
            _zoneService = zoneService;
        }

        [HttpGet]
        [TypeFilter(typeof(CourierTokenFilter))]
        public async Task<ActionResult<RestaurantDto>> GetInfo(long restaurantId)
        {
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

        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<RestaurantsDto>> GetAll()
        {
            try
            {
                var restaurantsDto = await _restaurantService.GetAll();
                return restaurantsDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<LatLngsDto>> GetZone(long restaurantId)
        {
            try
            {
                var latLngsDto = await _zoneService.GetRestaurantZone(restaurantId);
                return latLngsDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<LatLngsDto>> SetZone([FromBody] SetRestaurantZoneDto setRestaurantZoneDto)
        {
            try
            {
                await _zoneService.SetRestaurantZone(setRestaurantZoneDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}