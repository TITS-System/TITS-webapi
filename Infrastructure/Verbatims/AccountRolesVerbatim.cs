using System.Collections.Generic;

namespace Infrastructure.Verbatims
{
    public class AccountRolesVerbatim
    {
        public const string Superuser = "Superuser";
        public const string Cook = "Cook";
        public const string CookHot = "CookHot";
        public const string Packer = "Packer";
        public const string DeliveryCashier = "DeliveryCashier";
        public const string Manager = "Manager";
        public const string Client = "Client";

        public static Dictionary<string, string> EnToRu = new()
        {
            {Superuser, "Супер-пользователь"},
            {Cook, "Повар"},
            {CookHot, "Повар горячего цеха"},
            {Packer, "Упаковщик"},
            {DeliveryCashier, "Кассир доставки"},
            {Manager, "Менеджер"},
            {Client, "Клиент"}
        };

        public static Dictionary<string, string> RuToEn = new()
        {
            {"Супер-пользователь", Superuser},
            {"Повар", Cook},
            {"Повар горячего цеха", CookHot},
            {"Упаковщик", Packer},
            {"Кассир доставки", DeliveryCashier},
            {"Менеджер", Manager},
            {"Клиент", Client}
        };
    }
}