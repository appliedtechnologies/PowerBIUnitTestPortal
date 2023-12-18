using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class HistorySync : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestRun",
                table: "History",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_History_TestRun",
                table: "History",
                column: "TestRun");

            migrationBuilder.AddForeignKey(
                name: "FK_History_TestRuns_TestRun",
                table: "History",
                column: "TestRun",
                principalTable: "TestRuns",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_TestRuns_TestRun",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_TestRun",
                table: "History");

            migrationBuilder.DropColumn(
                name: "TestRun",
                table: "History");
        }
    }
}
