using System;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.Verbatims;
using Models.Db;
using Models.DTOs;
using Models.DTOs.Misc;
using Services.Abstractions;

namespace Services.Implementations
{
    public class WorkerSessionService : IWorkerSessionService
    {
        private IWorkerAccountRepository _workerAccountRepository;
        private IWorkerSessionRepository _workerSessionRepository;

        private IMapper _mapper;

        public WorkerSessionService(IWorkerAccountRepository workerAccountRepository, IWorkerSessionRepository workerSessionRepository, IMapper mapper)
        {
            _workerAccountRepository = workerAccountRepository;
            _workerSessionRepository = workerSessionRepository;
            _mapper = mapper;
        }

        public async Task<BeginWorkSessionResultDto> Begin(long workerId)
        {
            var workerAccount = await _workerAccountRepository.GetById(workerId);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            if (workerAccount.LastWorkerSessionId != null)
            {
                // возможно есть незакрытая смена
                var lastWorkerSession = await _workerSessionRepository.GetById(workerAccount.LastWorkerSessionId.Value);
                if (lastWorkerSession != null)
                {
                    // TODO: Возможно здесь её стоит закрыть
                    throw new(MessagesVerbatim.HasOpenWorkSession);
                }
            }

            if (workerAccount.MainRestaurantId == null)
            {
                throw new("Account is not assigned to restaurant yet");
            }

            WorkerSession ws = new WorkerSession()
            {
                WorkerAccount = workerAccount,
                IsClosed = false,
                RestaurantId = workerAccount.MainRestaurantId.Value, // save Id because we don't .Include(workpoint)
                OpenDateTime = DateTime.Now
            };

            await _workerSessionRepository.Insert(ws);

            workerAccount.LastWorkerSession = ws;

            await _workerAccountRepository.Update(workerAccount);

            return new BeginWorkSessionResultDto(ws.Id);
        }

        public async Task Close(long workerId)
        {
            var workerAccount = await _workerAccountRepository.GetById(workerId);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            if (workerAccount.LastWorkerSessionId == null)
            {
                throw new(MessagesVerbatim.NoOpenWorkSession);
            }

            // Load last worker session
            var lastWorkerSession = await _workerSessionRepository.GetById(workerAccount.LastWorkerSessionId.Value);

            // завершаем смену
            lastWorkerSession.IsClosed = true;
            lastWorkerSession.CloseDateTime = DateTime.Now;

            await _workerSessionRepository.Update(lastWorkerSession);

            // у пользователя больше нет последней активной рабочей сессии.
            workerAccount.LastWorkerSession = null;

            await _workerAccountRepository.Update(workerAccount);
        }

        public async Task<TimeDto> GetCurrentWorkerSessionDuration(long workerId)
        {
            var workerAccount = await _workerAccountRepository.GetById(workerId);

            if (workerAccount.LastWorkerSessionId == null)
            {
                throw new(MessagesVerbatim.NoOpenWorkSession);
            }

            // Load last worker session
            var lastWorkerSession = await _workerSessionRepository.GetById(workerAccount.LastWorkerSessionId.Value);

            var workSessionDuration = DateTime.Now - lastWorkerSession.OpenDateTime;

            return new TimeDto((long)workSessionDuration.TotalSeconds);
        }
    }
}