using System.Collections.Generic;

namespace Infrastructure.Verbatims
{
    public class WorkerRolesVerbatim
    {
        public const string Director = "Director";
        public const string Manager = "Manager";
        public const string Courier = "Courier";
        public const string Client = "Client";

        public static Dictionary<string, string> EnToRu = new()
        {
            {Director, "Директор"},
            {Manager, "Менеджер"},
            {Courier, "Курьер"},
            {Client, "Клиент"},
        };

        public static Dictionary<string, string> RuToEn = new()
        {
            {"Директор", Director},
            {"Менеджер", Manager},
            {"Курьер", Courier},
            {"Клиент", Client},
        };
    }
}