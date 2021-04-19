using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddOrderAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressAdditional",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressString",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Status",
                table: "Deliveries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressAdditional",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressString",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Deliveries");
        }
    }
}
