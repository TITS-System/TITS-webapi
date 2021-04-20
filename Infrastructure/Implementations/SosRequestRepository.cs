using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Models.Db;

namespace Infrastructure.Implementations
{
    public class SosRequestRepository : RepositoryBase, ISosRequestRepository
    {
        public SosRequestRepository(TitsDbContext context) : base(context)
        {
        }

        public async Task<SosRequest> GetById(long id)
        {
            return await Context.SosRequests.FindAsync(id);
        }

        public async Task Update(SosRequest sosRequest)
        {
            Context.SosRequests.Update(sosRequest);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(SosRequest sosRequest)
        {
            Context.SosRequests.Remove(sosRequest);
            await Context.SaveChangesAsync();
        }

        public async Task Insert(SosRequest sosRequest)
        {
            Context.SosRequests.Add(sosRequest);
            await Context.SaveChangesAsync();
        }
    }
}