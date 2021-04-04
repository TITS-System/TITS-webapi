using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Dtos.Requests;
using Models.Dtos.Responses;

namespace DodoHack.Areas.API
{
    public class OrderController : Controller
    {
        private DodoHackDbContext _context;
        private IMapper _mapper;

        public OrderController(DodoHackDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreatedDto>> Create([FromBody] CreateOrderDto orderDto)
        {
            var order = _mapper.Map<CreateOrderDto, Order>(orderDto);

            // Check that every template is present
            foreach (var templateId in orderDto.Templates)
            {
                if (await _context.OrderProductsTemplates.FindAsync(templateId) == null)
                {
                    return BadRequest($"Product Template {{{templateId}}} doesn't exist");
                }
            }

            order.Products = new List<OrderProduct>();

            foreach (var templateId in orderDto.Templates)
            {
                order.Products.Add(new() {ProductTemplateId = templateId});
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return new CreatedDto(order.Id);
        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> Get(long? id)
        {
            if (!id.HasValue)
            {
                return new BadRequestObjectResult($"This method requires '{nameof(id)}' parameter");
            }

            var order = await _context.Orders
                .Include(o => o.Destination)
                .Include(o => o.Courier)
                .Include(o => o.Products)
                .ThenInclude(p => p.ProductTemplate)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return new NotFoundObjectResult($"There is no object with '{nameof(id)}' = {id}");
            }

            var orderDto = _mapper.Map<Order, OrderDto>(order);

            return orderDto;
        }

        [HttpPost]
        public async Task<ActionResult> AssignCourier([FromBody] AssignOrderCourierDto assignOrderCourierDto)
        {
            var order = await _context.Orders.FindAsync(assignOrderCourierDto.OrderId);

            if (order == null)
            {
                return new NotFoundObjectResult(
                    $"There is no object with '{nameof(assignOrderCourierDto.OrderId)}' = {assignOrderCourierDto.OrderId}");
            }

            if (await _context.Couriers.FindAsync(assignOrderCourierDto.CourierId) == null)
            {
                return new NotFoundObjectResult(
                    $"There is no object with '{nameof(assignOrderCourierDto.CourierId)}' = {assignOrderCourierDto.CourierId}");
            }

            order.CourierId = assignOrderCourierDto.CourierId;
            await _context.SaveChangesAsync();
            return Ok("Success");
        }
    }
}