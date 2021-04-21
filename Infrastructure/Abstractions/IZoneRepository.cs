using System.Threading.Tasks;
using Models.Db;

namespace Infrastructure.Abstractions
{
    public interface IZoneRepository
    {
        Task<Zone> GetById(long id);

        Task Update(Zone zone);

        Task Remove(Zone zone);
        
        Task Insert(Zone zone);
    }
}