using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialGoalCalculator.Web.Migrations
{
    public partial class ChangeTableNameGeneralAssetCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstateAssetCase_Account_AccountId",
                table: "RealEstateAssetCase");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstateAssetCase_Scenario_ScenarioId",
                table: "RealEstateAssetCase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RealEstateAssetCase",
                table: "RealEstateAssetCase");

            migrationBuilder.RenameTable(
                name: "RealEstateAssetCase",
                newName: "GeneralAssetCase");

            migrationBuilder.RenameIndex(
                name: "IX_RealEstateAssetCase_ScenarioId",
                table: "GeneralAssetCase",
                newName: "IX_GeneralAssetCase_ScenarioId");

            migrationBuilder.RenameIndex(
                name: "IX_RealEstateAssetCase_AccountId",
                table: "GeneralAssetCase",
                newName: "IX_GeneralAssetCase_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GeneralAssetCase",
                table: "GeneralAssetCase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralAssetCase_Account_AccountId",
                table: "GeneralAssetCase",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralAssetCase_Scenario_ScenarioId",
                table: "GeneralAssetCase",
                column: "ScenarioId",
                principalTable: "Scenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralAssetCase_Account_AccountId",
                table: "GeneralAssetCase");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralAssetCase_Scenario_ScenarioId",
                table: "GeneralAssetCase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GeneralAssetCase",
                table: "GeneralAssetCase");

            migrationBuilder.RenameTable(
                name: "GeneralAssetCase",
                newName: "RealEstateAssetCase");

            migrationBuilder.RenameIndex(
                name: "IX_GeneralAssetCase_ScenarioId",
                table: "RealEstateAssetCase",
                newName: "IX_RealEstateAssetCase_ScenarioId");

            migrationBuilder.RenameIndex(
                name: "IX_GeneralAssetCase_AccountId",
                table: "RealEstateAssetCase",
                newName: "IX_RealEstateAssetCase_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RealEstateAssetCase",
                table: "RealEstateAssetCase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstateAssetCase_Account_AccountId",
                table: "RealEstateAssetCase",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstateAssetCase_Scenario_ScenarioId",
                table: "RealEstateAssetCase",
                column: "ScenarioId",
                principalTable: "Scenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
