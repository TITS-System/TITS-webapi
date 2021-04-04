using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dtos.General;
using Models.Dtos.Responses;

namespace DodoHack.Areas.API
{
    public class CourierController : Controller
    {
        private DodoHackDbContext _context;
        private IMapper _mapper;

        public CourierController(DodoHackDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreatedDto>> Create([FromBody] CreateCourierDto courierDto)
        {
            var courier = _mapper.Map<CreateCourierDto, Courier>(courierDto);
            await _context.Couriers.AddAsync(courier);
            await _context.SaveChangesAsync();

            return new CreatedDto(courier.Id);
        }

        [HttpGet]
        public async Task<ActionResult<CourierDto>> Get(long? id)
        {
            if (!id.HasValue)
            {
                return new BadRequestObjectResult($"This method requires '{nameof(id)}' parameter");
            }

            var courier = await _context.Couriers.FindAsync(id);

            if (courier == null)
            {
                return new NotFoundObjectResult($"There is no object with '{nameof(id)}' = {id}");
            }

            var courierDto = _mapper.Map<Courier, CourierDto>(courier);

            return courierDto;
        }
        
        
    }
}