using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Abstractions;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class AutoDeliveryServerController : TitsController
    {
        private IAutoDeliveryServerService _autoDeliveryServerService;
        public AutoDeliveryServerController(ITokenSessionService tokenSessionService, IAutoDeliveryServerService autoDeliveryServerService) : base(tokenSessionService)
        {
            _autoDeliveryServerService = autoDeliveryServerService;
        }
        
        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<bool>> GetState()
        {
            try
            {
                var mode = await _autoDeliveryServerService.GetMode();
                return mode;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
        
        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<bool>> Enable()
        {
            try
            {
                await _autoDeliveryServerService.SetAutoDeliveryMode(true);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
        
        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<bool>> Disable()
        {
            try
            {
                await _autoDeliveryServerService.SetAutoDeliveryMode(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}