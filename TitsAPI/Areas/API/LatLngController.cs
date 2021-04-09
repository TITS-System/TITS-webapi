using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Infrastructure.Verbatims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models.Dtos.General;
using Models.DTOs.Misc;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class LatLngController : TitsController
    {
        private IMapper _mapper;

        public LatLngController(TitsDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        [HttpPost]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<CreatedDto> Create([FromBody] LatLngDto latLngDto)
        {
            var accountSession = await GetRequestSession();

            LatLng latLng = new LatLng()
            {
                Lat = latLngDto.Lat,
                Lng = latLngDto.Lng,
                AccountId = accountSession.AccountId
            };

            await Context.LatLngs.AddAsync(latLng);
            await Context.SaveChangesAsync();

            return new CreatedDto(latLng.Id);
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<IEnumerable<LatLngDto>>> GetAll(long? accountId, long? limit)
        {
            if (accountId == null)
            {
                return TitsError("Pass GET accountId");
            }

            int useLimit = (int)(limit ?? 100000);

            var account = await Context.Accounts
                .Include(a => a.LatLngs.OrderByDescending(ll => ll.Id).Take(useLimit))
                .FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
            {
                return TitsError(MessagesVerbatim.AccountDoesntExist);
            }

            var latLngDtos = _mapper.Map<IEnumerable<LatLngDto>>(account.LatLngs.OrderBy(ll => ll.Id));

            return new ActionResult<IEnumerable<LatLngDto>>(latLngDtos);
        }
    }
}