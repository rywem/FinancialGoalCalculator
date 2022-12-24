using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialGoalCalculator.Web.Migrations
{
    public partial class MoreJobsColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CurrentlyEmployed",
                table: "Job",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Job",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentlyEmployed",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Job");
        }
    }
}
