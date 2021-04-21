using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Db;

namespace Infrastructure.Abstractions
{
    public interface ILatLngRepository
    {
        Task<LatLng> GetById(long id);

        Task Update(LatLng latLng);

        Task Remove(LatLng latLng);
        
        Task Remove(ICollection<LatLng> latLngs);

        Task Insert(LatLng latLng);
        
        Task Insert(ICollection<LatLng> latLngs);

        Task<ICollection<LatLng>> GetAllByDelivery(long deliveryId);
        
        Task<ICollection<LatLng>> GetAllByZone(long zoneId);
    }
}