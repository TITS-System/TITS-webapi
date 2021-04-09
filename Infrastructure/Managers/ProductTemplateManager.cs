using System.Collections.Generic;
using Models.Db;

namespace Infrastructure.Managers
{
    public class ProductTemplateManager
    {
        private TitsDbContext Context { get; set; }

        public ProductTemplateManager(TitsDbContext context)
        {
            Context = context;
        }

        public MenuProduct CreateProductTemplate(MenuProductPack menuProductPack, string title, float price, ProductCategory productCategory)
        {
            MenuProduct menuProduct = new()
            {
                Title = title,
                Price = price,
                ProductCategory = productCategory,
                Ingredients = new List<MenuIngredient>(),
                MenuProductPack = menuProductPack
            };
            Context.ProductTemplates.Add(menuProduct);
            Context.SaveChanges();
            return menuProduct;
        }
    }
}