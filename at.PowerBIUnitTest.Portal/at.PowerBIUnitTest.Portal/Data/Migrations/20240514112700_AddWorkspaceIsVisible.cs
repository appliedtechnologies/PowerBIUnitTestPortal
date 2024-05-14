using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class AddWorkspaceIsVisible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Is Visible",
                table: "Workspace",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is Visible",
                table: "Workspace");
        }
    }
}
