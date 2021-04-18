using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class AddOrderDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LastLatLngId",
                table: "WorkerAccounts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LatLngs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Lat = table.Column<float>(type: "real", nullable: false),
                    Lng = table.Column<float>(type: "real", nullable: false),
                    DeliveryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LatLngs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    DestinationLatLngId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_LatLngs_DestinationLatLngId",
                        column: x => x.DestinationLatLngId,
                        principalTable: "LatLngs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CourierAccountId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deliveries_WorkerAccounts_CourierAccountId",
                        column: x => x.CourierAccountId,
                        principalTable: "WorkerAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CourierAccountId",
                table: "Deliveries",
                column: "CourierAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_OrderId",
                table: "Deliveries",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LatLngs_DeliveryId",
                table: "LatLngs",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DestinationLatLngId",
                table: "Orders",
                column: "DestinationLatLngId");

            migrationBuilder.AddForeignKey(
                name: "FK_LatLngs_Deliveries_DeliveryId",
                table: "LatLngs",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LatLngs_Orders_Id",
                table: "LatLngs",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Orders_OrderId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_LatLngs_Orders_Id",
                table: "LatLngs");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "LatLngs");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropColumn(
                name: "LastLatLngId",
                table: "WorkerAccounts");
        }
    }
}
