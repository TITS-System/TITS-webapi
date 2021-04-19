using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class AddWorkerSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LatLngs_OrderId",
                table: "LatLngs");

            migrationBuilder.DropIndex(
                name: "IX_LatLngs_RestaurantId",
                table: "LatLngs");

            migrationBuilder.AddColumn<long>(
                name: "LastWorkerSessionId",
                table: "WorkerAccounts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MainRestaurantId",
                table: "WorkerAccounts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkerSessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    OpenDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CloseDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WorkerAccountId = table.Column<long>(type: "bigint", nullable: false),
                    RestaurantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerSessions_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerSessions_WorkerAccounts_WorkerAccountId",
                        column: x => x.WorkerAccountId,
                        principalTable: "WorkerAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAccounts_LastWorkerSessionId",
                table: "WorkerAccounts",
                column: "LastWorkerSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAccounts_MainRestaurantId",
                table: "WorkerAccounts",
                column: "MainRestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_LocationLatLngId",
                table: "Restaurants",
                column: "LocationLatLngId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DestinationLatLngId",
                table: "Orders",
                column: "DestinationLatLngId");

            migrationBuilder.CreateIndex(
                name: "IX_LatLngs_OrderId",
                table: "LatLngs",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LatLngs_RestaurantId",
                table: "LatLngs",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSessions_RestaurantId",
                table: "WorkerSessions",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSessions_WorkerAccountId",
                table: "WorkerSessions",
                column: "WorkerAccountId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAccounts_Restaurants_MainRestaurantId",
                table: "WorkerAccounts",
                column: "MainRestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAccounts_WorkerSessions_LastWorkerSessionId",
                table: "WorkerAccounts",
                column: "LastWorkerSessionId",
                principalTable: "WorkerSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LatLngs_DestinationLatLngId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_LatLngs_LocationLatLngId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerAccounts_Restaurants_MainRestaurantId",
                table: "WorkerAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerAccounts_WorkerSessions_LastWorkerSessionId",
                table: "WorkerAccounts");

            migrationBuilder.DropTable(
                name: "WorkerSessions");

            migrationBuilder.DropIndex(
                name: "IX_WorkerAccounts_LastWorkerSessionId",
                table: "WorkerAccounts");

            migrationBuilder.DropIndex(
                name: "IX_WorkerAccounts_MainRestaurantId",
                table: "WorkerAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_LocationLatLngId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DestinationLatLngId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_LatLngs_OrderId",
                table: "LatLngs");

            migrationBuilder.DropIndex(
                name: "IX_LatLngs_RestaurantId",
                table: "LatLngs");

            migrationBuilder.DropColumn(
                name: "LastWorkerSessionId",
                table: "WorkerAccounts");

            migrationBuilder.DropColumn(
                name: "MainRestaurantId",
                table: "WorkerAccounts");

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
        }
    }
}
