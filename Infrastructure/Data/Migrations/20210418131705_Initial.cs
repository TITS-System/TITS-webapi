using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkerRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleEn = table.Column<string>(type: "text", nullable: true),
                    TitleRu = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Password = table.Column<string>(type: "text", nullable: true),
                    LastTokenSessionId = table.Column<long>(type: "bigint", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Login = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TokenSessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkerAccountId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenSessions_WorkerAccounts_WorkerAccountId",
                        column: x => x.WorkerAccountId,
                        principalTable: "WorkerAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerAccountToRoles",
                columns: table => new
                {
                    WorkerAccountId = table.Column<long>(type: "bigint", nullable: false),
                    WorkerRoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerAccountToRoles", x => new { x.WorkerRoleId, x.WorkerAccountId });
                    table.ForeignKey(
                        name: "FK_WorkerAccountToRoles_WorkerAccounts_WorkerAccountId",
                        column: x => x.WorkerAccountId,
                        principalTable: "WorkerAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerAccountToRoles_WorkerRoles_WorkerRoleId",
                        column: x => x.WorkerRoleId,
                        principalTable: "WorkerRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TokenSessions_WorkerAccountId",
                table: "TokenSessions",
                column: "WorkerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAccounts_LastTokenSessionId",
                table: "WorkerAccounts",
                column: "LastTokenSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAccountToRoles_WorkerAccountId",
                table: "WorkerAccountToRoles",
                column: "WorkerAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAccounts_TokenSessions_LastTokenSessionId",
                table: "WorkerAccounts",
                column: "LastTokenSessionId",
                principalTable: "TokenSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TokenSessions_WorkerAccounts_WorkerAccountId",
                table: "TokenSessions");

            migrationBuilder.DropTable(
                name: "WorkerAccountToRoles");

            migrationBuilder.DropTable(
                name: "WorkerRoles");

            migrationBuilder.DropTable(
                name: "WorkerAccounts");

            migrationBuilder.DropTable(
                name: "TokenSessions");
        }
    }
}
