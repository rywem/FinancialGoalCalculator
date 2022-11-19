using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialGoalCalculator.Web.Migrations
{
    public partial class AddLoanRepaymentCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanRepaymentCase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExtraPayment = table.Column<decimal>(type: "TEXT", nullable: false),
                    PaymentInterval = table.Column<int>(type: "INTEGER", nullable: false),
                    ScenarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRepaymentCase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanRepaymentCase_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanRepaymentCase_Scenario_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "Scenario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepaymentCase_AccountId",
                table: "LoanRepaymentCase",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepaymentCase_ScenarioId",
                table: "LoanRepaymentCase",
                column: "ScenarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanRepaymentCase");
        }
    }
}
