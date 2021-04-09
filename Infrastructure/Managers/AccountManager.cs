using System;
using System.Linq;
using Models.Db;

namespace Infrastructure.Managers
{
    public class AccountManager
    {
        protected TitsDbContext Context { get; set; }

        public AccountManager(TitsDbContext context)
        {
            Context = context;
        }

        public Account CreateAccount(string login, string password, string username, WorkPoint mainWorkPoint)
        {
            if (Context.Accounts.Any(a => a.Login == login))
            {
                throw new ArgumentException($"Passed login '{login}' is already in use", nameof(login));
            }
            
            Account account = new()
            {
                Login = login,
                Password = password,
                Username = username,
                MainWorkPoint = mainWorkPoint
            };

            Context.Accounts.Add(account);
            Context.SaveChanges();
            return account;
        }
    }
}