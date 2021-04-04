using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dtos.General;
using Models.Dtos.Requests;
using Models.Dtos.Responses;

namespace DodoHack.Areas.API
{
    public class OrderProductTemplateController : Controller
    {
        private DodoHackDbContext _context;
        private IMapper _mapper;

        public OrderProductTemplateController(DodoHackDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<CreatedDto>> Create(
            [FromBody] OrderProductTemplateDto orderProductTemplateDto)
        {
            var orderProductTemplate =
                _mapper.Map<OrderProductTemplateDto, OrderProductTemplate>(orderProductTemplateDto);
            await _context.OrderProductsTemplates.AddAsync(orderProductTemplate);
            await _context.SaveChangesAsync();

            return new CreatedDto(orderProductTemplate.Id);
        }

        [HttpGet]
        public async Task<ActionResult<OrderProductTemplateDto>> Get(long? id)
        {
            if (!id.HasValue)
            {
                return new BadRequestObjectResult($"This method requires '{nameof(id)}' parameter");
            }

            var orderProductTemplate = await _context.OrderProductsTemplates.FindAsync(id);

            if (orderProductTemplate == null)
            {
                return new NotFoundObjectResult($"There is no object with '{nameof(id)}' = {id}");
            }

            var orderProductTemplateDto = _mapper.Map<OrderProductTemplate, OrderProductTemplateDto>(orderProductTemplate);

            return orderProductTemplateDto;
        }
    }
}