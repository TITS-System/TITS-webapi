using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.DTOs.Misc;
using Services.Abstractions;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class WorkerSessionController : TitsController
    {
        private ICourierSessionService _courierSessionService;

        public WorkerSessionController(ITokenSessionService tokenSessionService, ICourierSessionService courierSessionService) : base(tokenSessionService)
        {
            _courierSessionService = courierSessionService;
        }

        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<BeginCourierSessionResultDto>> Begin(long courierId)
        {
            try
            {
                var beginWorkSessionResultDto = await _courierSessionService.Begin(courierId);

                return beginWorkSessionResultDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<MessageDto>> Close(long courierId)
        {
            try
            {
                await _courierSessionService.Close(courierId);

                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<TimeDto>> GetCourierSessionDuration(long courierId)
        {
            try
            {
                var workSessionDurationDto = await _courierSessionService.GetDuration(courierId);

                return workSessionDurationDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}