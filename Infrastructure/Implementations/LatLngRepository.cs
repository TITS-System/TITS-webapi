using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Models.Db;

namespace Infrastructure.Implementations
{
    public class LatLngRepository : RepositoryBase, ILatLngRepository
    {
        public LatLngRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<LatLng> GetById(long id)
        {
            return await Context.LatLngs.FindAsync(id);
        }

        public async Task Update(LatLng latLng)
        {
            Context.LatLngs.Update(latLng);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(LatLng latLng)
        {
            Context.LatLngs.Remove(latLng);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(ICollection<LatLng> latLngs)
        {
            Context.LatLngs.RemoveRange(latLngs);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(LatLng latLng)
        {
            Context.LatLngs.Add(latLng);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(ICollection<LatLng> latLngs)
        {
            Context.AddRange(latLngs);
            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<LatLng>> GetAllByDelivery(long deliveryId)
        {
            return await Context.LatLngs.Where(ll => ll.DeliveryId == deliveryId).ToListAsync();
        }

        public async Task<ICollection<LatLng>> GetAllByZone(long zoneId)
        {
            return await Context.LatLngs.Where(ll => ll.ZoneId == zoneId).ToListAsync();
        }
    }
}