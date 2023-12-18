using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class LongerDax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DAX",
                table: "UnitTest",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                collation: "LATIN1_GENERAL_100_CI_AS_SC_UTF8",
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldMaxLength: 600,
                oldCollation: "LATIN1_GENERAL_100_CI_AS_SC_UTF8");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DAX",
                table: "UnitTest",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                collation: "LATIN1_GENERAL_100_CI_AS_SC_UTF8",
                oldClrType: typeof(string),
                oldType: "nvarchar(3000)",
                oldMaxLength: 3000,
                oldCollation: "LATIN1_GENERAL_100_CI_AS_SC_UTF8");
        }
    }
}
