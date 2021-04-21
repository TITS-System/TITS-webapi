using System.Threading.Tasks;
using Models.Dtos;

namespace Services.Abstractions
{
    public interface IStatsService
    {
        Task<StatsDto> BuildStats(long courierId);
    }
}