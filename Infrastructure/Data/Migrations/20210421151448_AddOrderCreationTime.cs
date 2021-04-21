using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddOrderCreationTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LastLatLngId",
                table: "Accounts",
                column: "LastLatLngId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_LatLngs_LastLatLngId",
                table: "Accounts",
                column: "LastLatLngId",
                principalTable: "LatLngs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_LatLngs_LastLatLngId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_LastLatLngId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "Orders");
        }
    }
}
