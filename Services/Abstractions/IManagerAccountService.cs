using System.Threading.Tasks;
using Models.DTOs.Misc;
using Models.DTOs.WorkerAccountDtos;

namespace Services.Abstractions
{
    public interface IManagerAccountService
    {
        Task<CreatedDto> CreateManager(CreateManagerAccountDto createManagerAccountDto);
    }
}