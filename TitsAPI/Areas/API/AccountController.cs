using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Infrastructure.Verbatims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models.DTOs.Misc;
using Models.DTOs.Requests;
using Models.DTOs.Responses;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class AccountController : TitsController
    {
        private IMapper _mapper;

        public AccountController(TitsDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await Task.Delay(0);
            return Ok("API/Account/Index");
        }

        [HttpPost]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginDto)
        {
            Account account = Context.Accounts.FirstOrDefault(a => a.Login == loginDto.Login);
            if (account == null)
            {
                return TitsError(MessagesVerbatim.AccountDoesntExist);
            }

            var lastSession = await Context.AccountSessions.AsNoTracking()
                .FirstOrDefaultAsync(session => session.Account == account && session.EndDate > DateTime.Now);

            if (lastSession == null)
            {
                // Create new session
                if (account.Password == loginDto.Password)
                {
                    // GUID gives something like this: 6668761a-7faf-44d0-b9cb-f20a57a66073

                    var endDate = loginDto.RememberMe ? DateTime.Now.AddDays(1) : DateTime.Now.AddHours(1);

                    AccountSession session = new()
                    {
                        Account = account,
                        Token = Guid.NewGuid().ToString(),
                        StartDate = DateTime.Now,
                        EndDate = endDate
                    };
                    await Context.AccountSessions.AddAsync(session);
                    await Context.SaveChangesAsync();

                    return new TokenDto(session.Token);
                }
                else
                {
                    return TitsError(MessagesVerbatim.PasswordInvalid);
                }
            }
            else
            {
                return new TokenDto(lastSession.Token);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<IEnumerable<AccountRoleDto>>> GetRoles()
        {
            var session = await GetRequestSession();

            var account = await Context.Accounts
                .Include(a => a.Roles)
                .ThenInclude(ar => ar.Role)
                .FirstOrDefaultAsync(a => a.Id == session.AccountId);

            if (account == null)
            {
                return TitsError(MessagesVerbatim.AccountNotFound);
            }

            var roles = account.Roles.Select(ar => ar.Role);

            var accountRoleDtos = _mapper.Map<IEnumerable<AccountRoleDto>>(roles);

            return new(accountRoleDtos);
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<MessageDto>> Logout()
        {
            var session = await GetRequestSession();

            // TODO: MAKE A FUCKING REPOSITORY FOR THIS KIND OF ACTIONS
            // #region Close Work Session
            //
            // var account = await Context.Accounts
            //     .Include(a => a.LastWorkSession)
            //     .ThenInclude(ws => ws.WorkSessionPauses)
            //     .FirstOrDefaultAsync(a => a.Id == session.AccountId);
            //
            // var lastSessionPause = account.LastWorkSession.WorkSessionPauses.OrderBy(wsp => wsp.Id).LastOrDefault();
            //
            // if (lastSessionPause != null && !lastSessionPause.IsClosed)
            // {
            //     lastSessionPause.EndDateTime = DateTime.Now;
            //     lastSessionPause.IsClosed = true;
            //     Context.WorkSessionPauses.Update(lastSessionPause);
            // }
            //
            // session.Account.LastWorkSession.CloseDateTime = DateTime.Now;
            // session.Account.LastWorkSession.IsClosed = true;
            // Context.WorkSessions.Update(session.Account.LastWorkSession);
            //
            // #endregion

            // Force end this token session 
            session.EndDate = DateTime.Now;
            await Context.SaveChangesAsync();

            return TitsMessage(MessagesVerbatim.Success);
        }
    }
}