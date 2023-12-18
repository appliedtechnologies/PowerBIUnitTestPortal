using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class HistoryCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_UnitTest_UnitTest",
                table: "History");

            migrationBuilder.AddForeignKey(
                name: "FK_History_UnitTest_UnitTest",
                table: "History",
                column: "UnitTest",
                principalTable: "UnitTest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_UnitTest_UnitTest",
                table: "History");

            migrationBuilder.AddForeignKey(
                name: "FK_History_UnitTest_UnitTest",
                table: "History",
                column: "UnitTest",
                principalTable: "UnitTest",
                principalColumn: "Id");
        }
    }
}
