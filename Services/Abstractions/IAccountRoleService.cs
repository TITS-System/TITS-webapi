using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAccountRoleService
    {
        Task AddToRole(long accountId, long roleId);

        Task AddToRole(long accountId, string roleTitleEn);

        Task RemoveFromRole(long accountId, long roleId);

        Task RemoveFromRole(long accountId, string roleTitleEn);
    }
}