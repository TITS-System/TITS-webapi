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

        public ProductPackTemplate CreateProductPack(string title, float price)
        {
            ProductPackTemplate productPackTemplate = new ProductPackTemplate()
            {
                Title = title,
                Price = price,
                Products = new List<ProductTemplate>()
            };
            Context.ProductPackTemplates.Add(productPackTemplate);
            Context.SaveChanges();
            return productPackTemplate;
        }
    }
}