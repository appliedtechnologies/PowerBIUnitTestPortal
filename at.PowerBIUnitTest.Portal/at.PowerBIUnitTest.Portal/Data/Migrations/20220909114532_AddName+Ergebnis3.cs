using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class AddNameErgebnis3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Letztes Ergebnis",
                table: "UnitTest",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                defaultValue: "Error",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Letzter Test",
                table: "UnitTest",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                defaultValue: "Error",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false,
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Letztes Ergebnis",
                table: "UnitTest",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValue: "Error");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Letzter Test",
                table: "UnitTest",
                type: "datetime2",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValue: "Error");
        }
    }
}
