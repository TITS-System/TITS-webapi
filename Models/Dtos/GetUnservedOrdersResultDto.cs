using System.Collections.Generic;

namespace Models.Dtos
{
    public class GetUnservedOrdersResultDto
    {
        public ICollection<UnservedOrderDto> Orders { get; set; }
    }
}