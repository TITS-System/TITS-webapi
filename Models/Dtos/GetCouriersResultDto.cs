using System.Collections.Generic;
using Models.Db.Account;

namespace Models.Dtos
{
    public class GetCouriersResultDto
    {
        public ICollection<CourierAccountDto> Couriers { get; set; }

        public GetCouriersResultDto(ICollection<CourierAccountDto> couriers)
        {
            Couriers = couriers;
        }
    }
}