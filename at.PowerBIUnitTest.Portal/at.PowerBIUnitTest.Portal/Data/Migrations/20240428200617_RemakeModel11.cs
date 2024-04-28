using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class RemakeModel11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Executed Successfully",
                table: "TestRun",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Json Repsonse",
                table: "TestRun",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Executed Successfully",
                table: "TestRun");

            migrationBuilder.DropColumn(
                name: "Json Repsonse",
                table: "TestRun");
        }
    }
}
