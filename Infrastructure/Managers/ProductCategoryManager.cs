using System.Linq;
using Models.Db;

namespace Infrastructure.Managers
{
    public class ProductCategoryManager
    {
        protected TitsDbContext Context { get; set; }

        public ProductCategoryManager(TitsDbContext context)
        {
            Context = context;
        }

        public ProductCategory FindByTitleEn(string titleEn)
        {
            var productCategory = Context.ProductCategories.FirstOrDefault(t => t.TitleEn == titleEn);
            return productCategory;
        }
    }
}