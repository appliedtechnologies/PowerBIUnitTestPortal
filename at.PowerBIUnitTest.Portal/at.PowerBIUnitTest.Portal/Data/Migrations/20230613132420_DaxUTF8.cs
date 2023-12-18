using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class DaxUTF8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DAX",
                table: "UnitTest",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                collation: "LATIN1_GENERAL_100_CI_AS_SC_UTF8",
                oldClrType: typeof(string),
                oldType: "varchar(600)",
                oldUnicode: false,
                oldMaxLength: 600);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DAX",
                table: "UnitTest",
                type: "varchar(600)",
                unicode: false,
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldMaxLength: 600,
                oldCollation: "LATIN1_GENERAL_100_CI_AS_SC_UTF8");
        }
    }
}
