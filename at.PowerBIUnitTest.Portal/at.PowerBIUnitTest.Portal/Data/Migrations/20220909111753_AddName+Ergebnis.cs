using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class AddNameErgebnis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ergebnis",
                table: "UnitTest",
                newName: "Letztes Ergebnis");

            migrationBuilder.AddColumn<string>(
                name: "Erwartetes Ergebnis",
                table: "UnitTest",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Letzter Test",
                table: "UnitTest",
                type: "datetime2",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TabularModel",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Erwartetes Ergebnis",
                table: "UnitTest");

            migrationBuilder.DropColumn(
                name: "Letzter Test",
                table: "UnitTest");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TabularModel");

            migrationBuilder.RenameColumn(
                name: "Letztes Ergebnis",
                table: "UnitTest",
                newName: "Ergebnis");
        }
    }
}
