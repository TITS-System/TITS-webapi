using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class AddSosRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SosRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourierAccountId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ResolveDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ResolverManagerAccountId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SosRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SosRequests_Accounts_CourierAccountId",
                        column: x => x.CourierAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SosRequests_Accounts_ResolverManagerAccountId",
                        column: x => x.ResolverManagerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SosRequests_CourierAccountId",
                table: "SosRequests",
                column: "CourierAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SosRequests_ResolverManagerAccountId",
                table: "SosRequests",
                column: "ResolverManagerAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SosRequests");
        }
    }
}
