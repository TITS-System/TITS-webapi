using System.Threading.Tasks;
using Infrastructure.Abstractions;
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

        public async Task Insert(LatLng latLng)
        {
            Context.LatLngs.Add(latLng);
            await Context.SaveChangesAsync();
        }
    }
}