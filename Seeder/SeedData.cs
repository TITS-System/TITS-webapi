using System;
using System.Linq;
using Infrastructure;
using Infrastructure.Managers;
using Infrastructure.Verbatims;
using Models.Db;

namespace Seeder
{
    public class SeedData
    {
        public SeedData(TitsDbContext context)
        {
            Context = context;
            AccountRoleManager = new AccountRoleManager(context);
            AccountManager = new AccountManager(context);
            IngredientTemplateManager = new IngredientTemplateManager(context);
            ProductCategoryManager = new ProductCategoryManager(context);
            ProductPackTemplateManager = new ProductPackTemplateManager(context);
            ProductTemplateManager = new ProductTemplateManager(context);
        }

        private TitsDbContext Context { get; set; }

        private AccountManager AccountManager { get; set; }

        private AccountRoleManager AccountRoleManager { get; set; }

        private IngredientTemplateManager IngredientTemplateManager { get; set; }

        private ProductCategoryManager ProductCategoryManager { get; set; }

        private ProductPackTemplateManager ProductPackTemplateManager { get; set; }

        private ProductTemplateManager ProductTemplateManager { get; set; }

        public void Seed()
        {
            SeedAccountRoles();
            SeedProductCategories();

            WorkPoint workPoint = new WorkPoint();
            WorkPoint defaultWorkPoint = new WorkPoint();
            Context.WorkPoints.Add(workPoint);
            Context.WorkPoints.Add(defaultWorkPoint);
            Context.SaveChanges();

            var admin = AccountManager.CreateAccount("Admin", "Admin", "SuperAdmin", workPoint);
            var courier = AccountManager.CreateAccount("Courier", "Courier", "SuperCourier", workPoint);
            var client = AccountManager.CreateAccount("Client", "Client", "SuperClient", defaultWorkPoint);
            
            AccountRoleManager.AddAccountRole(admin, AccountRolesVerbatim.Superuser);
            AccountRoleManager.AddAccountRole(courier, AccountRolesVerbatim.Courier);
            AccountRoleManager.AddAccountRole(client, AccountRolesVerbatim.Client);
            
            Console.WriteLine("Seeded accounts with roles");

            var productPackTemplate = ProductPackTemplateManager.CreateProductPack("Лосось 150г", 180);
            var productTemplate = ProductTemplateManager.CreateProductTemplate(
                productPackTemplate,
                "Лосось 150г",
                0,
                ProductCategoryManager.FindByTitleEn(ProductCategoryVerbatim.CommonRoll)
            );
            var ingredientTemplate = IngredientTemplateManager.CreateIngredientTemplate(productTemplate, "Рис", 100);
            
            Console.WriteLine("Seeded Product Pack Template");

            // Order order = new Order() {CreationDateTime = DateTime.Now};
            // Context.Orders.Add(order);
            // Context.SaveChanges();
            //
            // OrderProductPack orderProductPack = new OrderProductPack()
            // {
            //     Order = order,
            //     Title = productPackTemplate.Title,
            //     Price = productPackTemplate.Price
            // };
            // Context.OrderProductPacks.Add(orderProductPack);
            // Context.SaveChanges();
            //
            // OrderProduct orderProduct = new OrderProduct()
            // {
            //     ProductCategory = productTemplate.ProductCategory,
            //     ProductPack = orderProductPack,
            //     Price = productTemplate.Price,
            //     Title = productTemplate.Title
            // };
            //
            // Context.OrderProducts.Add(orderProduct);
            //
            // OrderIngredient orderIngredient = new OrderIngredient()
            // {
            //     Product = orderProduct,
            //     Title = ingredientTemplate.Title,
            //     Weight = ingredientTemplate.Weight
            // };
            //
            // Context.OrderIngredients.Add(orderIngredient);
            // Context.SaveChanges();

            ScheduledWorkSession scheduledWorkSession1 = new ScheduledWorkSession()
            {
                AccountId = client.Id,
                StartDateTime = DateTime.Today.AddHours(8), // today at 8 hours
                EndDateTime = DateTime.Today.AddHours(21), // today at 21 hours
                WorkPointId = workPoint.Id
            };
            
            Context.ScheduledWorkSessions.Add(scheduledWorkSession1);
            
            Console.WriteLine("Seeded scheduled worksessions");
            
            Context.SaveChanges();
        }

        private void SeedAccountRoles()
        {
            Context.AccountRoles.AddRange(
                from accountRoleEn in AccountRolesVerbatim.EnToRu.Keys
                select new AccountRole {TitleEn = accountRoleEn, TitleRu = AccountRolesVerbatim.EnToRu[accountRoleEn]}
            );
            Context.SaveChanges();
            Console.WriteLine("Seeded account roles");
        }

        private void SeedProductCategories()
        {
            Context.ProductCategories.AddRange(
                from productCategoryEn in ProductCategoryVerbatim.EnToRu.Keys
                select new ProductCategory
                    {TitleEn = productCategoryEn, TitleRu = ProductCategoryVerbatim.EnToRu[productCategoryEn]}
            );
            Context.SaveChanges();
            Console.WriteLine("Seeded product categories");
        }
    }
}