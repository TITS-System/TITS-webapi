using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Infrastructure.Implementations;
using Infrastructure.Verbatims;
using Models.Db;
using Models.Dtos;
using Services.Abstractions;

namespace Services.Implementations
{
    public class MessagingService : IMessagingService
    {
        private ICourierMessageRepository _courierMessageRepository;
        private ICourierAccountRepository _courierAccountRepository;
        private IMapper _mapper;

        public MessagingService(ICourierMessageRepository courierMessageRepository, ICourierAccountRepository courierAccountRepository, IMapper mapper)
        {
            _courierMessageRepository = courierMessageRepository;
            _courierAccountRepository = courierAccountRepository;
            _mapper = mapper;
        }

        public async Task Append(SendCourierMessageDto sendCourierMessageDto, bool isFromCourier)
        {
            var courierAccount = await _courierAccountRepository.GetById(sendCourierMessageDto.CourierId);
            
            if (courierAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            CourierMessage courierMessage = new CourierMessage()
            {
                Content = sendCourierMessageDto.Content,
                CourierAccount = courierAccount,
                CreationDateTime = DateTime.Now,
                IsFromCourier = isFromCourier
            };

            await _courierMessageRepository.Insert(courierMessage);
        }

        public async Task<GetCourierMessagesResultDto> GetHistory(long courierId, int limit, int offset)
        {
            var courierAccount = await _courierAccountRepository.GetById(courierId);

            if (courierAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            if (limit <= 0)
            {
                throw new("Invalid limit! Must be > 0.");
            }

            if (offset < 0)
            {
                throw new("Invalid offset! Must be >= 0.");
            }
            
            var courierMessages = await _courierMessageRepository.GetForCourier(courierId, limit, offset);

            var courierMessageDtos = _mapper.Map<ICollection<CourierMessageDto>>(courierMessages);

            return new GetCourierMessagesResultDto(courierMessageDtos);
        }
    }
}