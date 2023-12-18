using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class EngUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Letzter Test",
                table: "UnitTest",
                newName: "LastRun");

            migrationBuilder.RenameColumn(
                name: "Letztes Ergebnis",
                table: "UnitTest",
                newName: "LastResult");

            migrationBuilder.RenameColumn(
                name: "Erwartetes Ergebnis",
                table: "UnitTest",
                newName: "ExpectedResult");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastRun",
                table: "UnitTest",
                newName: "Letzter Test");

            migrationBuilder.RenameColumn(
                name: "LastResult",
                table: "UnitTest",
                newName: "Letztes Ergebnis");

            migrationBuilder.RenameColumn(
                name: "ExpectedResult",
                table: "UnitTest",
                newName: "Erwartetes Ergebnis");
        }
    }
}
