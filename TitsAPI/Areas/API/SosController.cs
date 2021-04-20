using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Db.TokenSessions;
using Models.Dtos;
using Models.DTOs.Misc;
using Services.Abstractions;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class SosController : TitsController
    {
        private ISosService _sosService;
        
        public SosController(ITokenSessionService tokenSessionService, ISosService sosService) : base(tokenSessionService)
        {
            _sosService = sosService;
        }
        
        [HttpPost]
        [TypeFilter(typeof(CourierTokenFilter))]
        public async Task<ActionResult<CreatedDto>> RequestSos(long courierId)
        {
            try
            {
                var createdDto = await _sosService.RequestSos(courierId);
                return createdDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult> ResolveSos(long sosId, long managerId)
        {
            try
            {
                await _sosService.ResolveSos(sosId, managerId);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(ManagerTokenFilter))]
        public async Task<ActionResult<SosDto>> GetSosInfo(long sosId)
        {
            try
            {
                var sosDto = await _sosService.GetInfo(sosId);
                return sosDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}