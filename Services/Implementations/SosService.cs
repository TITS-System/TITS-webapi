using System;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.Verbatims;
using Models.Db;
using Models.Dtos;
using Models.DTOs.Misc;
using Services.Abstractions;

namespace Services.Implementations
{
    public class SosService : ISosService
    {
        private ICourierAccountRepository _courierAccountRepository;
        private ISosRequestRepository _sosRequestRepository;

        private IMapper _mapper;

        public SosService(ICourierAccountRepository courierAccountRepository, ISosRequestRepository sosRequestRepository, IMapper mapper)
        {
            _courierAccountRepository = courierAccountRepository;
            _sosRequestRepository = sosRequestRepository;
            _mapper = mapper;
        }

        public async Task<CreatedDto> RequestSos(long courierId)
        {
            var courierAccount = await _courierAccountRepository.GetById(courierId);

            if (courierAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            SosRequest sosRequest = new SosRequest()
            {
                CourierAccountId = courierId,
                CreationDateTime = DateTime.Now
            };

            await _sosRequestRepository.Insert(sosRequest);

            return new CreatedDto(sosRequest.Id);
        }

        public async Task ResolveSos(long sosId, long managerId)
        {
            var sosRequest = await _sosRequestRepository.GetById(sosId);

            if (sosRequest == null)
            {
                throw new("SosRequest not found");
            }

            sosRequest.ResolverManagerAccountId = managerId;
            sosRequest.ResolveDateTime = DateTime.Now;

            await _sosRequestRepository.Update(sosRequest);
        }

        public async Task<SosDto> GetInfo(long sosId)
        {
            var sosRequest = await _sosRequestRepository.GetById(sosId);

            if (sosRequest == null)
            {
                throw new("SosRequest not found");
            }

            var sosDto = _mapper.Map<SosDto>(sosRequest);

            return sosDto;
        }
    }
}