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
        private IWorkerSessionService _workerSessionService;

        public WorkerSessionController(ITokenSessionService tokenSessionService, IWorkerSessionService workerSessionService) : base(tokenSessionService)
        {
            _workerSessionService = workerSessionService;
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<BeginWorkSessionResultDto>> Begin()
        {
            var tokenSession = await GetRequestSession();

            try
            {
                var beginWorkSessionResultDto = await _workerSessionService.Begin(tokenSession.WorkerAccountId);

                return beginWorkSessionResultDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<MessageDto>> Close()
        {
            var tokenSession = await GetRequestSession();

            try
            {
                await _workerSessionService.Close(tokenSession.WorkerAccountId);

                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<TimeDto>> GetCurrentWorkerSessionDuration()
        {
            var tokenSession = await GetRequestSession();

            try
            {
                var workSessionDurationDto = await _workerSessionService.GetCurrentWorkerSessionDuration(tokenSession.WorkerAccountId);

                return workSessionDurationDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}