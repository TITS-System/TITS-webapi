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
        public async Task<ActionResult<CreatedDto>> Create([FromBody] CreateOrderDto createOrderDto)
        {
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
        
        [HttpGet]
        [TypeFilter(typeof(CourierTokenFilter))]
        public async Task<ActionResult<GetUnservedOrdersResultDto>> GetUnserved(long restaurantId)
        {
            try
            {
                var getUnservedOrdersResultDto = await _orderService.GetUnserved(restaurantId);
                return getUnservedOrdersResultDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
        
        [HttpGet]
        [TypeFilter(typeof(CourierTokenFilter))]
        public async Task<ActionResult<OrderDto>> GetInfo(long orderId)
        {
            try
            {
                var orderDto = await _orderService.GetInfo(orderId);
                return orderDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}