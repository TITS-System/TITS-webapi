using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class improve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "OrderProducts");

            migrationBuilder.AddColumn<long>(
                name: "ProductTemplateId",
                table: "OrderProducts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "OrderProductsTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductsTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductTemplateId",
                table: "OrderProducts",
                column: "ProductTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_OrderProductsTemplates_ProductTemplateId",
                table: "OrderProducts",
                column: "ProductTemplateId",
                principalTable: "OrderProductsTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_OrderProductsTemplates_ProductTemplateId",
                table: "OrderProducts");

            migrationBuilder.DropTable(
                name: "OrderProductsTemplates");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_ProductTemplateId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "ProductTemplateId",
                table: "OrderProducts");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "OrderProducts",
                type: "text",
                nullable: true);
        }
    }
}
