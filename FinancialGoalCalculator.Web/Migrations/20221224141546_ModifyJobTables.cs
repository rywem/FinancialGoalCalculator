using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialGoalCalculator.Web.Migrations
{
    public partial class ModifyJobTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Job",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Job",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Job",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "Job");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Job",
                newName: "Name");
        }
    }
}
