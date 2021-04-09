using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Verbatims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models.DTOs.Misc;
using TitsAPI.Controllers;
using TitsAPI.Filters;

namespace TitsAPI.Areas.API
{
    public class WorkSessionController : TitsController
    {
        public WorkSessionController(TitsDbContext context) : base(context)
        {
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<CreatedDto>> Begin()
        {
            var requestSession = await GetRequestSession();

            var account = requestSession.Account;
            
            // TODO: Optimize every-time query to a temporary storage service for all active sessions and their pauses
            
            await Context.Entry(account)
                .Reference(a => a.LastWorkSession)
                .Query()
                .Include(ws => ws.WorkSessionPauses)
                .LoadAsync();

            var activeSession = account.LastWorkSession;

            if (activeSession == null)
            {
                var scheduledWorkSession = await Context.ScheduledWorkSessions
                    .FirstOrDefaultAsync(wst =>
                        wst.Account == account && wst.StartDateTime < DateTime.Now && wst.EndDateTime > DateTime.Now);

                WorkSession ws = new WorkSession()
                {
                    Account = account,
                    IsClosed = false,
                    WorkPointId = account.MainWorkPointId, // save Id because we don't .Include(workpoint)
                    ScheduledWorkSessionId = scheduledWorkSession?.Id,
                    OpenDateTime = DateTime.Now
                };

                await Context.WorkSessions.AddAsync(ws);
                account.LastWorkSession = ws;
                await Context.SaveChangesAsync();

                return new CreatedDto(ws.Id);
            }
            else
            {
                return TitsError(MessagesVerbatim.HasOpenWorkSession);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<MessageDto>> Close()
        {
            var requestSession = await GetRequestSession();

            var account = requestSession.Account;

            await Context.Entry(account)
                .Reference(a => a.LastWorkSession)
                .Query()
                .Include(ws => ws.WorkSessionPauses)
                .LoadAsync();

            var activeSession = account.LastWorkSession;

            if (activeSession == null)
            {
                return TitsError(MessagesVerbatim.NoOpenWorkSession);
            }

            var lastSessionPause = activeSession.WorkSessionPauses.OrderBy(wsp => wsp.Id).LastOrDefault();

            if (lastSessionPause != null && !lastSessionPause.IsClosed)
            {
                lastSessionPause.EndDateTime = DateTime.Now;
                lastSessionPause.IsClosed = true;
            }

            activeSession.IsClosed = true;
            activeSession.CloseDateTime = DateTime.Now;

            account.LastWorkSession = null;

            await Context.SaveChangesAsync();

            return TitsMessage(MessagesVerbatim.Success);
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<MessageDto>> Pause()
        {
            var requestSession = await GetRequestSession();

            var account = requestSession.Account;

            await Context.Entry(account)
                .Reference(a => a.LastWorkSession)
                .Query()
                .Include(ws => ws.WorkSessionPauses)
                .LoadAsync();

            var activeSession = account.LastWorkSession;

            if (activeSession == null)
            {
                return TitsError(MessagesVerbatim.NoOpenWorkSession);
            }

            var lastSessionPause = activeSession.WorkSessionPauses.OrderBy(wsp => wsp.Id).LastOrDefault();

            if (lastSessionPause != null && !lastSessionPause.IsClosed)
            {
                return TitsError(MessagesVerbatim.WorkSessionIsAlreadyPaused);
            }

            WorkSessionPause workSessionPause = new WorkSessionPause()
            {
                StartDateTime = DateTime.Now,
                WorkSession = activeSession,
                IsClosed = false
            };

            await Context.WorkSessionPauses.AddAsync(workSessionPause);
            await Context.SaveChangesAsync();

            return TitsMessage(MessagesVerbatim.Success);
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<MessageDto>> UnPause()
        {
            var requestSession = await GetRequestSession();

            var account = requestSession.Account;

            await Context.Entry(account)
                .Reference(a => a.LastWorkSession)
                .Query()
                .Include(ws => ws.WorkSessionPauses)
                .LoadAsync();

            var activeSession = account.LastWorkSession;

            if (activeSession == null)
            {
                return TitsError(MessagesVerbatim.NoOpenWorkSession);
            }

            var lastWorkSessionPause = activeSession.WorkSessionPauses.OrderBy(wsp => wsp.Id).LastOrDefault();

            if (lastWorkSessionPause == null || lastWorkSessionPause.IsClosed)
            {
                return TitsError(MessagesVerbatim.WorkSessionIsNotPaused);
            }

            lastWorkSessionPause.IsClosed = true;
            lastWorkSessionPause.EndDateTime = DateTime.Now;

            await Context.SaveChangesAsync();

            return TitsMessage(MessagesVerbatim.Success);
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<TimeDto>> GetCurrentPauseDuration()
        {
            var requestSession = await GetRequestSession();

            var account = requestSession.Account;
            
            await Context.Entry(account)
                .Reference(a => a.LastWorkSession)
                .Query()
                .Include(ws => ws.WorkSessionPauses)
                .LoadAsync();

            var activeSession = account.LastWorkSession;

            if (activeSession == null)
            {
                return TitsError(MessagesVerbatim.NoOpenWorkSession);
            }

            var lastWorkSessionPause = activeSession.WorkSessionPauses.OrderBy(wsp => wsp.Id).LastOrDefault();

            if (lastWorkSessionPause == null || lastWorkSessionPause.IsClosed)
            {
                return new TimeDto((long)-1);
            }

            var pauseDuration = DateTime.Now - lastWorkSessionPause.StartDateTime;

            return new TimeDto((long)pauseDuration.TotalSeconds);
        }

        [HttpGet]
        [TypeFilter(typeof(CheckAuthTokenFilter))]
        public async Task<ActionResult<TimeDto>> GetCurrentWorkSessionDuration()
        {
            var requestSession = await GetRequestSession();

            var account = requestSession.Account;

            await Context.Entry(account)
                .Reference(a => a.LastWorkSession)
                .Query()
                .Include(ws => ws.WorkSessionPauses)
                .LoadAsync();

            var activeSession = account.LastWorkSession;

            if (activeSession == null)
            {
                return TitsError(MessagesVerbatim.NoOpenWorkSession);
            }

            var workSessionDuration = DateTime.Now - activeSession.OpenDateTime;

            return new TimeDto((long)workSessionDuration.TotalSeconds);
        }
    }
}