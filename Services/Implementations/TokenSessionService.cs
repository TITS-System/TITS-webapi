using System;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Infrastructure.Verbatims;
using Models.Db.TokenSessions;
using Models.Dtos;
using Models.DTOs.Requests;
using Services.Abstractions;

namespace Services.Implementations
{
    public class TokenSessionService : ITokenSessionService
    {
        private ICourierTokenSessionRepository _courierTokenSessionRepository;
        private IManagerTokenSessionRepository _managerTokenSessionRepository;
        private ICourierAccountRepository _courierAccountRepository;
        private IManagerAccountRepository _managerAccountRepository;

        public TokenSessionService(ICourierTokenSessionRepository courierTokenSessionRepository, IManagerTokenSessionRepository managerTokenSessionRepository, ICourierAccountRepository courierAccountRepository, IManagerAccountRepository managerAccountRepository)
        {
            _courierTokenSessionRepository = courierTokenSessionRepository;
            _managerTokenSessionRepository = managerTokenSessionRepository;
            _courierAccountRepository = courierAccountRepository;
            _managerAccountRepository = managerAccountRepository;
        }

        public async Task<LoginResultDto> LoginCourier(LoginDto loginDto)
        {
            var courierAccount = await _courierAccountRepository.GetByLogin(loginDto.Login);

            if (courierAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            if (courierAccount.Password != loginDto.Password)
            {
                throw new(MessagesVerbatim.PasswordInvalid);
            }

            if (courierAccount.LastTokenSessionId != null)
            {
                // Found an unclosed last session
                var lastTokenSession = await _courierTokenSessionRepository.GetById(courierAccount.LastTokenSessionId.Value);

                if (lastTokenSession.EndDate > DateTime.Now)
                {
                    return new LoginResultDto(courierAccount.Id, lastTokenSession.Token);
                }
                
                // Don't close old session, it's automatically expired by EndDate
            }

            // Create new Token Session

            var endDate = DateTime.Now.AddDays(1);

            CourierTokenSession session = new()
            {
                CourierAccount = courierAccount,
                Token = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now,
                EndDate = endDate
            };

            await _courierTokenSessionRepository.Insert(session);

            // Save token session in user
            courierAccount.LastTokenSessionId = session.Id;
            await _courierAccountRepository.Update(courierAccount);

            return new LoginResultDto(courierAccount.Id, session.Token);
        }

        public async Task<LoginResultDto> LoginManager(LoginDto loginDto)
        {
            var managerAccount = await _managerAccountRepository.GetByLogin(loginDto.Login);

            if (managerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            if (managerAccount.Password != loginDto.Password)
            {
                throw new(MessagesVerbatim.PasswordInvalid);
            }

            if (managerAccount.LastTokenSessionId != null)
            {
                // Found an unclosed last session
                var lastTokenSession = await _managerTokenSessionRepository.GetById(managerAccount.LastTokenSessionId.Value);

                if (lastTokenSession.EndDate > DateTime.Now)
                {
                    return new LoginResultDto(managerAccount.Id, lastTokenSession.Token);
                }
                
                // Don't close old session, it's automatically expired by EndDate
            }

            // Create new Token Session

            var endDate = DateTime.Now.AddDays(1);

            ManagerTokenSession session = new()
            {
                ManagerAccount = managerAccount,
                Token = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now,
                EndDate = endDate
            };

            await _managerTokenSessionRepository.Insert(session);

            // Save token session in user
            managerAccount.LastTokenSessionId = session.Id;
            await _managerAccountRepository.Update(managerAccount);

            return new LoginResultDto(managerAccount.Id, session.Token);
        }

        public async Task<CourierTokenSession> GetCourierSessionByToken(string token)
        {
            return await _courierTokenSessionRepository.GetByToken(token);
        }

        public async Task<ManagerTokenSession> GetManagerSessionByToken(string token)
        {
            return await _managerTokenSessionRepository.GetByToken(token);
        }

        public async Task Logout(CourierTokenSession courierTokenSession)
        {
            courierTokenSession.EndDate = DateTime.Now;
            await _courierTokenSessionRepository.Update(courierTokenSession);
        }

        public async Task Logout(ManagerTokenSession managerTokenSession)
        {
            managerTokenSession.EndDate = DateTime.Now;
            await _managerTokenSessionRepository.Update(managerTokenSession);
        }
    }
}