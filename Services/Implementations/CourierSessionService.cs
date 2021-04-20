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
    public class CourierSessionService : ICourierSessionService
    {
        private ICourierAccountRepository _courierAccountRepository;
        private ICourierSessionRepository _courierSessionRepository;

        private IMapper _mapper;

        public CourierSessionService(ICourierAccountRepository courierAccountRepository, ICourierSessionRepository courierSessionRepository, IMapper mapper)
        {
            _courierAccountRepository = courierAccountRepository;
            _courierSessionRepository = courierSessionRepository;
            _mapper = mapper;
        }

        public async Task<BeginCourierSessionResultDto> Begin(long courierId)
        {
            var courierAccount = await _courierAccountRepository.GetById(courierId);

            if (courierAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            if (courierAccount.LastCourierSessionId != null)
            {
                // возможно есть незакрытая смена
                var lastCourierSession = await _courierSessionRepository.GetById(courierAccount.LastCourierSessionId.Value);
                if (lastCourierSession != null)
                {
                    // TODO: Возможно здесь её стоит закрыть
                    throw new(MessagesVerbatim.HasOpenWorkSession);
                }
            }

            // if (courierAccount.AssignedToRestaurantId == null)
            // {
            //     throw new("Account is not assigned to restaurant yet");
            // }

            CourierSession ws = new CourierSession()
            {
                CourierAccount = courierAccount,
                IsClosed = false,
                OpenDateTime = DateTime.Now
            };

            await _courierSessionRepository.Insert(ws);

            courierAccount.LastCourierSession = ws;

            await _courierAccountRepository.Update(courierAccount);

            return new BeginCourierSessionResultDto(ws.Id);
        }

        public async Task Close(long courierId)
        {
            var courierAccount = await _courierAccountRepository.GetById(courierId);

            if (courierAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            if (courierAccount.LastCourierSessionId == null)
            {
                throw new(MessagesVerbatim.NoOpenWorkSession);
            }

            // Load last worker session
            var lastCourierSession = await _courierSessionRepository.GetById(courierAccount.LastCourierSessionId.Value);

            // завершаем смену
            lastCourierSession.IsClosed = true;
            lastCourierSession.CloseDateTime = DateTime.Now;

            await _courierSessionRepository.Update(lastCourierSession);

            // у пользователя больше нет последней активной рабочей сессии.
            courierAccount.LastCourierSession = null;

            await _courierAccountRepository.Update(courierAccount);
        }

        public async Task<TimeDto> GetDuration(long courierId)
        {
            var courierAccount = await _courierAccountRepository.GetById(courierId);

            if (courierAccount.LastCourierSessionId == null)
            {
                throw new(MessagesVerbatim.NoOpenWorkSession);
            }

            // Load last worker session
            var lastCourierSession = await _courierSessionRepository.GetById(courierAccount.LastCourierSessionId.Value);

            var courierSessionDuration = DateTime.Now - lastCourierSession.OpenDateTime;

            return new TimeDto((long)courierSessionDuration.TotalSeconds);
        }
    }
}