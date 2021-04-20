using System.Threading.Tasks;
using Models.Db;

namespace Infrastructure.Abstractions
{
    public interface ISosRequestRepository
    {
        Task<SosRequest> GetById(long id);

        Task Update(SosRequest sosRequest);

        Task Remove(SosRequest sosRequest);

        Task Insert(SosRequest sosRequest);
    }
}