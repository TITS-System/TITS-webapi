using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IWorkerRoleService
    {
        Task AddToRole(long workerId, long roleId);

        Task AddToRole(long workerId, string roleTitleEn);

        Task RemoveFromRole(long workerId, long roleId);

        Task RemoveFromRole(long workerId, string roleTitleEn);
    }
}