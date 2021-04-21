using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Abstractions;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class MessagingController : TitsController
    {
        private IMessagingService _messagingService;

        public MessagingController(ITokenSessionService tokenSessionService, IMessagingService messagingService) : base(tokenSessionService)
        {
            _messagingService = messagingService;
        }

        [HttpPost]
        [TypeFilter(typeof(CourierTokenFilter))]
        public async Task<ActionResult> CSend([FromBody] SendCourierMessageDto sendCourierMessageDto)
        {
            try
            {
                await _messagingService.Append(sendCourierMessageDto, true);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult> MSend([FromBody] SendCourierMessageDto sendCourierMessageDto)
        {
            try
            {
                await _messagingService.Append(sendCourierMessageDto, false);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<GetCourierMessagesResultDto>> MGetHistory(long courierId, int limit = 25, int offset = 0)
        {
            try
            {
                var getCourierMessagesResultDto = await _messagingService.GetHistory(courierId, limit, offset);
                return getCourierMessagesResultDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CourierTokenFilter))]
        public async Task<ActionResult<GetCourierMessagesResultDto>> CGetHistory(long courierId, int limit = 25, int offset = 0)
        {
            try
            {
                var getCourierMessagesResultDto = await _messagingService.GetHistory(courierId, limit, offset);
                return getCourierMessagesResultDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}