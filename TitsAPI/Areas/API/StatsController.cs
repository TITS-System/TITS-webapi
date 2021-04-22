using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Abstractions;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class StatsController : TitsController
    {
        private IStatsService _statsService;
        public StatsController(ITokenSessionService tokenSessionService, IStatsService statsService) : base(tokenSessionService)
        {
            _statsService = statsService;
        }
        
        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<StatsDto>> MGetStats(long courierId)
        {
            try
            {
                var statsDto = await _statsService.BuildStats(courierId);
                return statsDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
        
        [HttpGet]
        [TypeFilter(typeof(CourierTokenFilter))]
        public async Task<ActionResult<StatsDto>> CGetStats(long courierId)
        {
            try
            {
                var statsDto = await _statsService.BuildStats(courierId);
                return statsDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}