using System.Threading.Tasks;
using Models.Dtos;
using Models.DTOs.Misc;

namespace Services.Abstractions
{
    public interface ISosService
    {
        Task<CreatedDto> RequestSos(long courierId);

        Task ResolveSos(long courierId, long managerId);

        Task<SosDto> GetInfo(long sosId);
    }
}