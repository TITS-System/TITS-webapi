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

        public ProductTemplate CreateProductTemplate(ProductPackTemplate productPackTemplate, string title, float price, ProductCategory productCategory)
        {
            ProductTemplate productTemplate = new()
            {
                Title = title,
                Price = price,
                ProductCategory = productCategory,
                Ingredients = new List<IngredientTemplate>(),
                ProductPack = productPackTemplate
            };
            Context.ProductTemplates.Add(productTemplate);
            Context.SaveChanges();
            return productTemplate;
        }
    }
}