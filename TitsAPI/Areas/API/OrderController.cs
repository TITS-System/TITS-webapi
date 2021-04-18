using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models.DTOs.Misc;
using Services.Abstractions;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class OrderController : TitsController
    {
        private IOrderService _orderService;
        public OrderController(ITokenSessionService tokenSessionService, IOrderService orderService) : base(tokenSessionService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> Create([FromBody] CreateOrderDto createOrderDto)
        {
            var tokenSession = await GetRequestSession();

            try
            {
                var createdDto = await _orderService.Create(createOrderDto);
                return createdDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}