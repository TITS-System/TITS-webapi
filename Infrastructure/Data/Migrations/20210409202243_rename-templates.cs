using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class renametemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientTemplates_ProductTemplates_ProductId",
                table: "IngredientTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTemplates_ProductCategories_ProductCategoryId",
                table: "ProductTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTemplates_ProductPackTemplates_ProductPackId",
                table: "ProductTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTemplates",
                table: "ProductTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPackTemplates",
                table: "ProductPackTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientTemplates",
                table: "IngredientTemplates");

            migrationBuilder.RenameTable(
                name: "ProductTemplates",
                newName: "MenuProducts");

            migrationBuilder.RenameTable(
                name: "ProductPackTemplates",
                newName: "MenuProductPacks");

            migrationBuilder.RenameTable(
                name: "IngredientTemplates",
                newName: "MenuIngredients");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTemplates_ProductPackId",
                table: "MenuProducts",
                newName: "IX_MenuProducts_ProductPackId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTemplates_ProductCategoryId",
                table: "MenuProducts",
                newName: "IX_MenuProducts_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientTemplates_ProductId",
                table: "MenuIngredients",
                newName: "IX_MenuIngredients_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuProducts",
                table: "MenuProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuProductPacks",
                table: "MenuProductPacks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuIngredients",
                table: "MenuIngredients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuIngredients_MenuProducts_ProductId",
                table: "MenuIngredients",
                column: "ProductId",
                principalTable: "MenuProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuProducts_MenuProductPacks_ProductPackId",
                table: "MenuProducts",
                column: "ProductPackId",
                principalTable: "MenuProductPacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuProducts_ProductCategories_ProductCategoryId",
                table: "MenuProducts",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuIngredients_MenuProducts_ProductId",
                table: "MenuIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuProducts_MenuProductPacks_ProductPackId",
                table: "MenuProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuProducts_ProductCategories_ProductCategoryId",
                table: "MenuProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuProducts",
                table: "MenuProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuProductPacks",
                table: "MenuProductPacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuIngredients",
                table: "MenuIngredients");

            migrationBuilder.RenameTable(
                name: "MenuProducts",
                newName: "ProductTemplates");

            migrationBuilder.RenameTable(
                name: "MenuProductPacks",
                newName: "ProductPackTemplates");

            migrationBuilder.RenameTable(
                name: "MenuIngredients",
                newName: "IngredientTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_MenuProducts_ProductPackId",
                table: "ProductTemplates",
                newName: "IX_ProductTemplates_ProductPackId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuProducts_ProductCategoryId",
                table: "ProductTemplates",
                newName: "IX_ProductTemplates_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuIngredients_ProductId",
                table: "IngredientTemplates",
                newName: "IX_IngredientTemplates_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTemplates",
                table: "ProductTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPackTemplates",
                table: "ProductPackTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientTemplates",
                table: "IngredientTemplates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientTemplates_ProductTemplates_ProductId",
                table: "IngredientTemplates",
                column: "ProductId",
                principalTable: "ProductTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTemplates_ProductCategories_ProductCategoryId",
                table: "ProductTemplates",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTemplates_ProductPackTemplates_ProductPackId",
                table: "ProductTemplates",
                column: "ProductPackId",
                principalTable: "ProductPackTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
