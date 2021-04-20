using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class ModelsBringup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_WorkerAccounts_CourierAccountId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_TokenSessions_WorkerAccounts_WorkerAccountId",
                table: "TokenSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerAccountToRoles_WorkerAccounts_WorkerAccountId",
                table: "WorkerAccountToRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerSessions_Restaurants_RestaurantId",
                table: "WorkerSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerSessions_WorkerAccounts_WorkerAccountId",
                table: "WorkerSessions");

            migrationBuilder.DropTable(
                name: "WorkerAccounts");

            migrationBuilder.DropIndex(
                name: "IX_WorkerSessions_RestaurantId",
                table: "WorkerSessions");

            migrationBuilder.DropIndex(
                name: "IX_TokenSessions_WorkerAccountId",
                table: "TokenSessions");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "WorkerSessions");

            migrationBuilder.DropColumn(
                name: "WorkerAccountId",
                table: "TokenSessions");

            migrationBuilder.RenameColumn(
                name: "WorkerAccountId",
                table: "WorkerSessions",
                newName: "CourierAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerSessions_WorkerAccountId",
                table: "WorkerSessions",
                newName: "IX_WorkerSessions_CourierAccountId");

            migrationBuilder.AddColumn<long>(
                name: "AccountBaseId",
                table: "WorkerAccountToRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CourierAccountId",
                table: "TokenSessions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "TokenSessions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ManagerAccountId",
                table: "TokenSessions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    LastTokenSessionId = table.Column<long>(type: "bigint", nullable: true),
                    AssignedToRestaurantId = table.Column<long>(type: "bigint", nullable: true),
                    LastLatLngId = table.Column<long>(type: "bigint", nullable: true),
                    LastCourierSessionId = table.Column<long>(type: "bigint", nullable: true),
                    ManagerAccount_LastTokenSessionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Restaurants_AssignedToRestaurantId",
                        column: x => x.AssignedToRestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_TokenSessions_LastTokenSessionId",
                        column: x => x.LastTokenSessionId,
                        principalTable: "TokenSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_TokenSessions_ManagerAccount_LastTokenSessionId",
                        column: x => x.ManagerAccount_LastTokenSessionId,
                        principalTable: "TokenSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_WorkerSessions_LastCourierSessionId",
                        column: x => x.LastCourierSessionId,
                        principalTable: "WorkerSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAccountToRoles_AccountBaseId",
                table: "WorkerAccountToRoles",
                column: "AccountBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenSessions_CourierAccountId",
                table: "TokenSessions",
                column: "CourierAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenSessions_ManagerAccountId",
                table: "TokenSessions",
                column: "ManagerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AssignedToRestaurantId",
                table: "Accounts",
                column: "AssignedToRestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LastCourierSessionId",
                table: "Accounts",
                column: "LastCourierSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LastTokenSessionId",
                table: "Accounts",
                column: "LastTokenSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ManagerAccount_LastTokenSessionId",
                table: "Accounts",
                column: "ManagerAccount_LastTokenSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Accounts_CourierAccountId",
                table: "Deliveries",
                column: "CourierAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TokenSessions_Accounts_CourierAccountId",
                table: "TokenSessions",
                column: "CourierAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TokenSessions_Accounts_ManagerAccountId",
                table: "TokenSessions",
                column: "ManagerAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAccountToRoles_Accounts_AccountBaseId",
                table: "WorkerAccountToRoles",
                column: "AccountBaseId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAccountToRoles_Accounts_WorkerAccountId",
                table: "WorkerAccountToRoles",
                column: "WorkerAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerSessions_Accounts_CourierAccountId",
                table: "WorkerSessions",
                column: "CourierAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Accounts_CourierAccountId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_TokenSessions_Accounts_CourierAccountId",
                table: "TokenSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_TokenSessions_Accounts_ManagerAccountId",
                table: "TokenSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerAccountToRoles_Accounts_AccountBaseId",
                table: "WorkerAccountToRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerAccountToRoles_Accounts_WorkerAccountId",
                table: "WorkerAccountToRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerSessions_Accounts_CourierAccountId",
                table: "WorkerSessions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_WorkerAccountToRoles_AccountBaseId",
                table: "WorkerAccountToRoles");

            migrationBuilder.DropIndex(
                name: "IX_TokenSessions_CourierAccountId",
                table: "TokenSessions");

            migrationBuilder.DropIndex(
                name: "IX_TokenSessions_ManagerAccountId",
                table: "TokenSessions");

            migrationBuilder.DropColumn(
                name: "AccountBaseId",
                table: "WorkerAccountToRoles");

            migrationBuilder.DropColumn(
                name: "CourierAccountId",
                table: "TokenSessions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "TokenSessions");

            migrationBuilder.DropColumn(
                name: "ManagerAccountId",
                table: "TokenSessions");

            migrationBuilder.RenameColumn(
                name: "CourierAccountId",
                table: "WorkerSessions",
                newName: "WorkerAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerSessions_CourierAccountId",
                table: "WorkerSessions",
                newName: "IX_WorkerSessions_WorkerAccountId");

            migrationBuilder.AddColumn<long>(
                name: "RestaurantId",
                table: "WorkerSessions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WorkerAccountId",
                table: "TokenSessions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "WorkerAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastLatLngId = table.Column<long>(type: "bigint", nullable: true),
                    LastTokenSessionId = table.Column<long>(type: "bigint", nullable: true),
                    LastWorkerSessionId = table.Column<long>(type: "bigint", nullable: true),
                    Login = table.Column<string>(type: "text", nullable: true),
                    MainRestaurantId = table.Column<long>(type: "bigint", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerAccounts_Restaurants_MainRestaurantId",
                        column: x => x.MainRestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkerAccounts_TokenSessions_LastTokenSessionId",
                        column: x => x.LastTokenSessionId,
                        principalTable: "TokenSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkerAccounts_WorkerSessions_LastWorkerSessionId",
                        column: x => x.LastWorkerSessionId,
                        principalTable: "WorkerSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSessions_RestaurantId",
                table: "WorkerSessions",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenSessions_WorkerAccountId",
                table: "TokenSessions",
                column: "WorkerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAccounts_LastTokenSessionId",
                table: "WorkerAccounts",
                column: "LastTokenSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAccounts_LastWorkerSessionId",
                table: "WorkerAccounts",
                column: "LastWorkerSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerAccounts_MainRestaurantId",
                table: "WorkerAccounts",
                column: "MainRestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_WorkerAccounts_CourierAccountId",
                table: "Deliveries",
                column: "CourierAccountId",
                principalTable: "WorkerAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TokenSessions_WorkerAccounts_WorkerAccountId",
                table: "TokenSessions",
                column: "WorkerAccountId",
                principalTable: "WorkerAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerAccountToRoles_WorkerAccounts_WorkerAccountId",
                table: "WorkerAccountToRoles",
                column: "WorkerAccountId",
                principalTable: "WorkerAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerSessions_Restaurants_RestaurantId",
                table: "WorkerSessions",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerSessions_WorkerAccounts_WorkerAccountId",
                table: "WorkerSessions",
                column: "WorkerAccountId",
                principalTable: "WorkerAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
