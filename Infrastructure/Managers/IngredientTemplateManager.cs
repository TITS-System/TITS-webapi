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

        public IngredientTemplate CreateIngredientTemplate(ProductTemplate productTemplate, string title, int weightGrams)
        {
            IngredientTemplate ingredientTemplate = new()
            {
                Title = title,
                Weight = weightGrams,
                Product = productTemplate
            };
            Context.IngredientTemplates.Add(ingredientTemplate);
            Context.SaveChanges();
            return ingredientTemplate;
        }
    }
}