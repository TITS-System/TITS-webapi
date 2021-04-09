using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class migratesystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LatLngs_Couriers_CourierId",
                table: "LatLngs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_OrderProductsTemplates_ProductTemplateId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Couriers_CourierId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LatLngs_DestinationId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Couriers");

            migrationBuilder.DropTable(
                name: "OrderProductsTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CourierId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DestinationId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_LatLngs_CourierId",
                table: "LatLngs");

            migrationBuilder.DropColumn(
                name: "CourierId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CourierId",
                table: "LatLngs");

            migrationBuilder.RenameColumn(
                name: "ProductTemplateId",
                table: "OrderProducts",
                newName: "ProductPackId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderProducts",
                newName: "ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductTemplateId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductPackId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductCategoryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "OrderProducts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "OrderProducts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleEn = table.Column<string>(type: "text", nullable: true),
                    TitleRu = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderIngredients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderIngredients_OrderProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "OrderProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProductPacks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductPacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProductPacks_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleEn = table.Column<string>(type: "text", nullable: true),
                    TitleRu = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPackTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkPoints",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ProductPackId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTemplates_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTemplates_ProductPackTemplates_ProductPackId",
                        column: x => x.ProductPackId,
                        principalTable: "ProductPackTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientTemplates_ProductTemplates_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountToRoles",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountToRoles", x => new { x.RoleId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_AccountToRoles_AccountRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccountRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountSessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledWorkSessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    WorkPointId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledWorkSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledWorkSessions_WorkPoints_WorkPointId",
                        column: x => x.WorkPointId,
                        principalTable: "WorkPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkSessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScheduledWorkSessionId = table.Column<long>(type: "bigint", nullable: true),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    OpenDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CloseDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    WorkPointId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSessions_ScheduledWorkSessions_ScheduledWorkSessionId",
                        column: x => x.ScheduledWorkSessionId,
                        principalTable: "ScheduledWorkSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSessions_WorkPoints_WorkPointId",
                        column: x => x.WorkPointId,
                        principalTable: "WorkPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    MainWorkPointId = table.Column<long>(type: "bigint", nullable: false),
                    LastWorkSessionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_WorkPoints_MainWorkPointId",
                        column: x => x.MainWorkPointId,
                        principalTable: "WorkPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_WorkSessions_LastWorkSessionId",
                        column: x => x.LastWorkSessionId,
                        principalTable: "WorkSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSessionPauses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkSessionId = table.Column<long>(type: "bigint", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSessionPauses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSessionPauses_WorkSessions_WorkSessionId",
                        column: x => x.WorkSessionId,
                        principalTable: "WorkSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LastWorkSessionId",
                table: "Accounts",
                column: "LastWorkSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_MainWorkPointId",
                table: "Accounts",
                column: "MainWorkPointId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSessions_AccountId",
                table: "AccountSessions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountToRoles_AccountId",
                table: "AccountToRoles",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientTemplates_ProductId",
                table: "IngredientTemplates",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderIngredients_ProductId",
                table: "OrderIngredients",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductPacks_OrderId",
                table: "OrderProductPacks",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTemplates_ProductCategoryId",
                table: "ProductTemplates",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTemplates_ProductPackId",
                table: "ProductTemplates",
                column: "ProductPackId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkSessions_AccountId",
                table: "ScheduledWorkSessions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledWorkSessions_WorkPointId",
                table: "ScheduledWorkSessions",
                column: "WorkPointId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSessionPauses_WorkSessionId",
                table: "WorkSessionPauses",
                column: "WorkSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSessions_AccountId",
                table: "WorkSessions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSessions_ScheduledWorkSessionId",
                table: "WorkSessions",
                column: "ScheduledWorkSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSessions_WorkPointId",
                table: "WorkSessions",
                column: "WorkPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_OrderProductPacks_ProductPackId",
                table: "OrderProducts",
                column: "ProductPackId",
                principalTable: "OrderProductPacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_ProductCategories_ProductCategoryId",
                table: "OrderProducts",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountToRoles_Accounts_AccountId",
                table: "AccountToRoles",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountSessions_Accounts_AccountId",
                table: "AccountSessions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledWorkSessions_Accounts_AccountId",
                table: "ScheduledWorkSessions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSessions_Accounts_AccountId",
                table: "WorkSessions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_OrderProductPacks_ProductPackId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_ProductCategories_ProductCategoryId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_WorkPoints_MainWorkPointId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledWorkSessions_WorkPoints_WorkPointId",
                table: "ScheduledWorkSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSessions_WorkPoints_WorkPointId",
                table: "WorkSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_WorkSessions_LastWorkSessionId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountSessions");

            migrationBuilder.DropTable(
                name: "AccountToRoles");

            migrationBuilder.DropTable(
                name: "IngredientTemplates");

            migrationBuilder.DropTable(
                name: "OrderIngredients");

            migrationBuilder.DropTable(
                name: "OrderProductPacks");

            migrationBuilder.DropTable(
                name: "WorkSessionPauses");

            migrationBuilder.DropTable(
                name: "AccountRoles");

            migrationBuilder.DropTable(
                name: "ProductTemplates");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductPackTemplates");

            migrationBuilder.DropTable(
                name: "WorkPoints");

            migrationBuilder.DropTable(
                name: "WorkSessions");

            migrationBuilder.DropTable(
                name: "ScheduledWorkSessions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "ProductPackId",
                table: "OrderProducts",
                newName: "ProductTemplateId");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryId",
                table: "OrderProducts",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductPackId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductCategoryId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_OrderId");

            migrationBuilder.AddColumn<long>(
                name: "CourierId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DestinationId",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CourierId",
                table: "LatLngs",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Couriers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.Id);
                });

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
                name: "IX_Orders_CourierId",
                table: "Orders",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DestinationId",
                table: "Orders",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_LatLngs_CourierId",
                table: "LatLngs",
                column: "CourierId");

            migrationBuilder.AddForeignKey(
                name: "FK_LatLngs_Couriers_CourierId",
                table: "LatLngs",
                column: "CourierId",
                principalTable: "Couriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_OrderProductsTemplates_ProductTemplateId",
                table: "OrderProducts",
                column: "ProductTemplateId",
                principalTable: "OrderProductsTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Couriers_CourierId",
                table: "Orders",
                column: "CourierId",
                principalTable: "Couriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LatLngs_DestinationId",
                table: "Orders",
                column: "DestinationId",
                principalTable: "LatLngs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
