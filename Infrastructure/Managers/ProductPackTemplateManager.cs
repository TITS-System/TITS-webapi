using System.Collections.Generic;
using Models.Db;

namespace Infrastructure.Managers
{
    public class ProductPackTemplateManager
    {
        protected TitsDbContext Context { get; set; }

        public ProductPackTemplateManager(TitsDbContext context)
        {
            Context = context;
        }

        public MenuProductPack CreateProductPack(string title, float price)
        {
            MenuProductPack menuProductPack = new MenuProductPack()
            {
                Title = title,
                Price = price,
                Products = new List<MenuProduct>()
            };
            Context.MenuProductPacks.Add(menuProductPack);
            Context.SaveChanges();
            return menuProductPack;
        }
    }
}