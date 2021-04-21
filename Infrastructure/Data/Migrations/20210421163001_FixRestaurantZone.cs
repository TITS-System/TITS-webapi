using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class FixRestaurantZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "RestaurantZoneId",
                table: "LatLngs",
                newName: "ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_LatLngs_RestaurantZoneId",
                table: "LatLngs",
                newName: "IX_LatLngs_ZoneId");

            migrationBuilder.AddColumn<long>(
                name: "ZoneId",
                table: "Restaurants",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_LocationLatLngId",
                table: "Restaurants",
                column: "LocationLatLngId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_ZoneId",
                table: "Restaurants",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_LatLngs_RestaurantLocationId",
                table: "LatLngs",
                column: "RestaurantLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_RestaurantId",
                table: "Zones",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_LatLngs_Restaurants_RestaurantLocationId",
                table: "LatLngs",
                column: "RestaurantLocationId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LatLngs_Zones_ZoneId",
                table: "LatLngs",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_LatLngs_LocationLatLngId",
                table: "Restaurants",
                column: "LocationLatLngId",
                principalTable: "LatLngs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Zones_ZoneId",
                table: "Restaurants",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LatLngs_Restaurants_RestaurantLocationId",
                table: "LatLngs");

            migrationBuilder.DropForeignKey(
                name: "FK_LatLngs_Zones_ZoneId",
                table: "LatLngs");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_LatLngs_LocationLatLngId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Zones_ZoneId",
                table: "Restaurants");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_LocationLatLngId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_ZoneId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_LatLngs_RestaurantLocationId",
                table: "LatLngs");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "ZoneId",
                table: "LatLngs",
                newName: "RestaurantZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_LatLngs_ZoneId",
                table: "LatLngs",
                newName: "IX_LatLngs_RestaurantZoneId");

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
    }
}
