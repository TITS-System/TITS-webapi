using System.Collections.Generic;

namespace Infrastructure.Verbatims
{
    public class AccountRolesVerbatim
    {
        public const string Superuser = "Superuser";
        public const string Client = "Client";
        public const string Courier = "Courier";

        public static Dictionary<string, string> EnToRu = new()
        {
            {Superuser, "Супер-пользователь"},
            {Client, "Клиент"},
            {Courier, "Курьер"},
        };

        public static Dictionary<string, string> RuToEn = new()
        {
            {"Супер-пользователь", Superuser},
            {"Клиент", Client},
            {"Курьер", Courier},
        };
    }
}