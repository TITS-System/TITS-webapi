using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddRestaurantZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LatLngs_Restaurants_RestaurantId",
                table: "LatLngs");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_LatLngs_LocationLatLngId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_LocationLatLngId",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "LatLngs",
                newName: "RestaurantZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_LatLngs_RestaurantId",
                table: "LatLngs",
                newName: "IX_LatLngs_RestaurantZoneId");

            migrationBuilder.AddColumn<long>(
                name: "RestaurantLocationId",
                table: "LatLngs",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_LocationLatLngId",
                table: "Restaurants",
                column: "LocationLatLngId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LatLngs_Restaurants_RestaurantZoneId",
                table: "LatLngs",
                column: "RestaurantZoneId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_LatLngs_LocationLatLngId",
                table: "Restaurants",
                column: "LocationLatLngId",
                principalTable: "LatLngs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LatLngs_Restaurants_RestaurantZoneId",
                table: "LatLngs");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_LatLngs_LocationLatLngId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_LocationLatLngId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "RestaurantLocationId",
                table: "LatLngs");

            migrationBuilder.RenameColumn(
                name: "RestaurantZoneId",
                table: "LatLngs",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_LatLngs_RestaurantZoneId",
                table: "LatLngs",
                newName: "IX_LatLngs_RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_LocationLatLngId",
                table: "Restaurants",
                column: "LocationLatLngId");

            migrationBuilder.AddForeignKey(
                name: "FK_LatLngs_Restaurants_RestaurantId",
                table: "LatLngs",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
