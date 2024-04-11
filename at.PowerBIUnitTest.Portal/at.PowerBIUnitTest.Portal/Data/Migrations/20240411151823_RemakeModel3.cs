using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class RemakeModel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestRun_TestRunCollection_TestRun",
                table: "TestRun");

            migrationBuilder.RenameColumn(
                name: "TestRun",
                table: "TestRun",
                newName: "TestRunCollection");

            migrationBuilder.RenameIndex(
                name: "IX_TestRun_TestRun",
                table: "TestRun",
                newName: "IX_TestRun_TestRunCollection");

            migrationBuilder.AddForeignKey(
                name: "FK_TestRun_TestRunCollection_TestRunCollection",
                table: "TestRun",
                column: "TestRunCollection",
                principalTable: "TestRunCollection",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestRun_TestRunCollection_TestRunCollection",
                table: "TestRun");

            migrationBuilder.RenameColumn(
                name: "TestRunCollection",
                table: "TestRun",
                newName: "TestRun");

            migrationBuilder.RenameIndex(
                name: "IX_TestRun_TestRunCollection",
                table: "TestRun",
                newName: "IX_TestRun_TestRun");

            migrationBuilder.AddForeignKey(
                name: "FK_TestRun_TestRunCollection_TestRun",
                table: "TestRun",
                column: "TestRun",
                principalTable: "TestRunCollection",
                principalColumn: "Id");
        }
    }
}
