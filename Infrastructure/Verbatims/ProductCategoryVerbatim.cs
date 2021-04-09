using System.Collections.Generic;

namespace Infrastructure.Verbatims
{
    public class ProductCategoryVerbatim
    {
        public const string CommonRoll = "CommonRoll";
        public const string HotRoll = "HotRoll";
        public const string Soup = "Soup";
        public const string Drink = "Drink";
        public const string Addition = "Addition";

        public static Dictionary<string, string> EnToRu = new()
        {
            {CommonRoll, "Обычный ролл"},
            {HotRoll, "Печёный ролл"},
            {Soup, "Суп"},
            {Drink, "Напиток"},
            {Addition, "Допник"},
        };

        public static Dictionary<string, string> RuToEn = new()
        {
            {"Обычный ролл", CommonRoll},
            {"Печёный ролл", HotRoll},
            {"Суп", Soup},
            {"Напиток", Drink},
            {"Допник", Addition},
        };
    }
}