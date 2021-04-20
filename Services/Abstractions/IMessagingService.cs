using System.Threading.Tasks;
using Models.Dtos;

namespace Services.Abstractions
{
    public interface IMessagingService
    {
        Task Append(SendCourierMessageDto sendCourierMessageDto);

        Task<GetCourierMessagesResultDto> GetHistory(long courierId, int limit, int offset);
    }
}