using System.Collections.Generic;

namespace Infrastructure.Verbatims
{
    public class WorkerRolesVerbatim
    {
        public const string Manager = "Manager";
        public const string Client = "Client";
        public const string Courier = "Courier";

        public static Dictionary<string, string> EnToRu = new()
        {
            {Manager, "Менеджер"},
            {Client, "Клиент"},
            {Courier, "Курьер"},
        };

        public static Dictionary<string, string> RuToEn = new()
        {
            {"Менеджер", Manager},
            {"Клиент", Client},
            {"Курьер", Courier},
        };
    }
}