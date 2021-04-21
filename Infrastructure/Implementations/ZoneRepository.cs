using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Models.Db;

namespace Infrastructure.Implementations
{
    public class ZoneRepository : RepositoryBase, IZoneRepository
    {
        public ZoneRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<Zone> GetById(long id)
        {
            return await Context.Zones.FindAsync(id);
        }

        public async Task Update(Zone zone)
        {
            Context.Zones.Update(zone);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(Zone zone)
        {
            Context.Zones.Remove(zone);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(Zone zone)
        {
            Context.Zones.Add(zone);
            await Context.SaveChangesAsync();
        }
    }
}