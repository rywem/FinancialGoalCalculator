using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialGoalCalculator.Web.Migrations
{
    public partial class AddLoanDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    OriginalBalance = table.Column<decimal>(type: "TEXT", nullable: false),
                    FirstPaymentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InterestRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Periods = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanDetail_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetail_AccountId",
                table: "LoanDetail",
                column: "AccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanDetail");
        }
    }
}
