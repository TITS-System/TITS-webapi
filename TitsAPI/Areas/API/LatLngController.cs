using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models.Db;
using Models.Dtos.General;
using Models.DTOs.Misc;

namespace TitsAPI.Areas.API
{
    public class LatLngController
    {
        private TitsDbContext _context;
        private IMapper _mapper;

        public LatLngController(TitsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreatedDto>> Create([FromBody] LatLngDto createLatLngDto)
        {
            LatLng latLng = _mapper.Map<LatLngDto, LatLng>(createLatLngDto);
            await _context.LatLngs.AddAsync(latLng);
            await _context.SaveChangesAsync();

            return new CreatedDto(latLng.Id);
        }

        [HttpGet]
        public async Task<ActionResult<LatLngDto>> Get(long? id)
        {
            if (!id.HasValue)
            {
                return new BadRequestObjectResult($"This method requires '{nameof(id)}' parameter");
            }
            
            LatLng latLng = await _context.LatLngs.FindAsync(id);

            if (latLng == null)
            {
                return new NotFoundObjectResult($"There is no object with '{nameof(id)}' = {id}");
            }

            var latLngDto = _mapper.Map<LatLng, LatLngDto>(latLng);

            return latLngDto;
        }
    }
}