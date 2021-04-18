using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class AddLatLngsConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LatLngs_Orders_Id",
                table: "LatLngs");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LatLngs_DestinationLatLngId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_LatLngs_LocationLatLngId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_LocationLatLngId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DestinationLatLngId",
                table: "Orders");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "LatLngs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "LatLngs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RestaurantId",
                table: "LatLngs",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LatLngs_OrderId",
                table: "LatLngs",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LatLngs_RestaurantId",
                table: "LatLngs",
                column: "RestaurantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LatLngs_Orders_OrderId",
                table: "LatLngs",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LatLngs_Restaurants_RestaurantId",
                table: "LatLngs",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LatLngs_Orders_OrderId",
                table: "LatLngs");

            migrationBuilder.DropForeignKey(
                name: "FK_LatLngs_Restaurants_RestaurantId",
                table: "LatLngs");

            migrationBuilder.DropIndex(
                name: "IX_LatLngs_OrderId",
                table: "LatLngs");

            migrationBuilder.DropIndex(
                name: "IX_LatLngs_RestaurantId",
                table: "LatLngs");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "LatLngs");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "LatLngs");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "LatLngs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_LocationLatLngId",
                table: "Restaurants",
                column: "LocationLatLngId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DestinationLatLngId",
                table: "Orders",
                column: "DestinationLatLngId");

            migrationBuilder.AddForeignKey(
                name: "FK_LatLngs_Orders_Id",
                table: "LatLngs",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LatLngs_DestinationLatLngId",
                table: "Orders",
                column: "DestinationLatLngId",
                principalTable: "LatLngs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_LatLngs_LocationLatLngId",
                table: "Restaurants",
                column: "LocationLatLngId",
                principalTable: "LatLngs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
