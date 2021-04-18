using System;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Infrastructure.Verbatims;
using Models.Db.Sessions;
using Models.Dtos;
using Models.DTOs.Requests;
using Services.Abstractions;

namespace Services.Implementations
{
    public class TokenSessionService : ITokenSessionService
    {
        private ITokenSessionRepository _tokenSessionRepository;
        private IWorkerAccountRepository _workerAccountRepository;

        public TokenSessionService(ITokenSessionRepository tokenSessionRepository, IWorkerAccountRepository workerAccountRepository)
        {
            _tokenSessionRepository = tokenSessionRepository;
            _workerAccountRepository = workerAccountRepository;
        }

        public async Task<LoginResultDto> Login(LoginDto loginDto)
        {
            var workerAccount = await _workerAccountRepository.GetByLogin(loginDto.Login);

            if (workerAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            if (workerAccount.Password != loginDto.Password)
            {
                throw new(MessagesVerbatim.PasswordInvalid);
            }

            if (workerAccount.LastTokenSessionId != null)
            {
                // Found an unclosed last session
                var lastTokenSession = await _tokenSessionRepository.GetById(workerAccount.LastTokenSessionId.Value);

                if (lastTokenSession.EndDate < DateTime.Now)
                {
                    return new LoginResultDto(workerAccount.Id, lastTokenSession.Token);
                }
                
                // Don't close old session, it's automatically expired by EndDate
            }

            // Create new Token Session

            var endDate = DateTime.Now.AddMinutes(10);

            TokenSession session = new()
            {
                WorkerAccount = workerAccount,
                Token = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now,
                EndDate = endDate
            };

            await _tokenSessionRepository.Insert(session);

            // Save token session in user
            workerAccount.LastTokenSessionId = session.Id;
            await _workerAccountRepository.Update(workerAccount);

            return new LoginResultDto(workerAccount.Id, session.Token);
        }

        public async Task<TokenSession> GetByToken(string token)
        {
            return await _tokenSessionRepository.GetByToken(token);
        }

        public async Task Logout(TokenSession tokenSession)
        {
            tokenSession.EndDate = DateTime.Now;
            await _tokenSessionRepository.Update(tokenSession);
        }
    }
}