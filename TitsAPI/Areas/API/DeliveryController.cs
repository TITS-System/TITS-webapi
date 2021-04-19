using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Abstractions;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class DeliveryController : TitsController
    {
        private IDeliveryService _deliveryService;

        public DeliveryController(ITokenSessionService tokenSessionService, IDeliveryService deliveryService) : base(tokenSessionService)
        {
            _deliveryService = deliveryService;
        }

        [HttpPost]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult> Begin([FromBody] BeginDeliveryDto beginDeliveryDto)
        {
            try
            {
                await _deliveryService.BeginDelivery(beginDeliveryDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult> Finish(long deliveryId)
        {
            try
            {
                await _deliveryService.FinishDelivery(deliveryId);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult> Cancel(long deliveryId)
        {
            try
            {
                await _deliveryService.CancelDelivery(deliveryId);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<DeliveriesDto>> GetByCourier(long courierId)
        {
            try
            {
                var deliveriesDto = await _deliveryService.GetAllByCourier(courierId);
                return deliveriesDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<LatLngsDto>> GetLocations(long deliveryId)
        {
            try
            {
                var latLngsDto = await _deliveryService.GetDeliveryLocations(deliveryId);
                return latLngsDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult> AddLocation([FromBody] AddDeliveryLocationDto addDeliveryLocationDto)
        {
            try
            {
                await _deliveryService.AddDeliveryLocation(addDeliveryLocationDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }
    }
}