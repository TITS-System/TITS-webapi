using System.Threading.Tasks;
using Models.Db;

namespace Infrastructure.Abstractions
{
    public interface ILatLngRepository
    {
        Task<LatLng> GetById(long id);

        Task Update(LatLng latLng);

        Task Remove(LatLng latLng);

        Task Insert(LatLng latLng);
    }
}