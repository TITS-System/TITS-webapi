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
        [TypeFilter(typeof(CourierTokenFilter))]
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
        [TypeFilter(typeof(CourierTokenFilter))]
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
        [TypeFilter(typeof(CourierTokenFilter))]
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

        [HttpPost]
        [TypeFilter(typeof(CourierTokenFilter))]
        public async Task<ActionResult<DeliveriesDto>> GetByCourierAndDates(GetByCourierAndDateDto getByCourierAndDateDto)
        {
            try
            {
                var deliveriesDto = await _deliveryService.GetAllByCourierAndDate(getByCourierAndDateDto);
                return deliveriesDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CourierTokenFilter))]
        public async Task<ActionResult<DeliveriesDto>> GetInProgressByCourier(long? courierId)
        {
            try
            {
                if (courierId == null)
                {
                    throw new("No courierId passed");
                }
                var deliveriesDto = await _deliveryService.GetInProgressByCourier(courierId.Value);
                return deliveriesDto;
            }
            catch (Exception ex)
            {
                return TitsError(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CourierTokenFilter))]
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
        [TypeFilter(typeof(CourierTokenFilter))]
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