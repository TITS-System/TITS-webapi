using System;
using System.Linq;
using Models.Db;

namespace Infrastructure.Managers
{
    public class AccountRoleManager
    {
        private TitsDbContext _context;

        public AccountRoleManager(TitsDbContext context)
        {
            _context = context;
        }

        public void AddAccountRole(Account account, string roleEn)
        {
            var accountRole = _context.AccountRoles.FirstOrDefault(r => r.TitleEn == roleEn);

            if (accountRole == null)
            {
                throw new ArgumentOutOfRangeException(nameof(roleEn), roleEn, null);
            }

            _context.Entry(account).Collection(a => a.Roles).Load();

            if (account.Roles.Any(r => r.RoleId == accountRole.Id))
            {
                throw new ArgumentOutOfRangeException($"Account already in role {roleEn}");
            }
            else
            {
                AccountToRole accountToRole = new() {Account = account, Role = accountRole};
                _context.AccountToRoles.Add(accountToRole);
                _context.SaveChanges();
            }
        }

        public void RemoveAccountRole(Account account, string roleEn)
        {
            var accountRole = _context.AccountRoles.FirstOrDefault(r => r.TitleEn == roleEn);

            if (accountRole == null)
            {
                throw new ArgumentOutOfRangeException(nameof(roleEn), roleEn, null);
            }

            var accountToRole =
                _context.AccountToRoles.FirstOrDefault(role => role.Account == account && role.Role == accountRole);

            if (accountToRole != null)
            {
                _context.AccountToRoles.Remove(accountToRole);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Account is not in role");
            }
        }
    }
}