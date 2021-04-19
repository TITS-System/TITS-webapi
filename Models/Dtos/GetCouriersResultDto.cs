using System.Collections.Generic;
using Models.Db.Account;

namespace Models.Dtos
{
    public class GetCouriersResultDto
    {
        public ICollection<WorkerAccountDto> Couriers { get; set; }

        public GetCouriersResultDto(ICollection<WorkerAccountDto> couriers)
        {
            Couriers = couriers;
        }
    }
}