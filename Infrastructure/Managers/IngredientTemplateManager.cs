using Models.Db;

namespace Infrastructure.Managers
{
    public class IngredientTemplateManager
    {
        private TitsDbContext Context { get; set; }

        public IngredientTemplateManager(TitsDbContext context)
        {
            Context = context;
        }

        public MenuIngredient CreateIngredientTemplate(MenuProduct menuProduct, string title, int weightGrams)
        {
            MenuIngredient menuIngredient = new()
            {
                Title = title,
                Weight = weightGrams,
                MenuProduct = menuProduct
            };
            Context.MenuIngredients.Add(menuIngredient);
            Context.SaveChanges();
            return menuIngredient;
        }
    }
}