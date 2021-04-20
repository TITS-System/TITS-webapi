using System.Collections.Generic;

namespace Models.Dtos
{
    public class GetCourierMessagesResultDto
    {
        public ICollection<CourierMessageDto> Messages { get; set; }

        public GetCourierMessagesResultDto(ICollection<CourierMessageDto> messages)
        {
            Messages = messages;
        }
    }
}