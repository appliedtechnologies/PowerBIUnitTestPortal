using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class AllCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabularModel_Workspace_Workspace",
                table: "TabularModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitTest_UserStory_UserStory",
                table: "UnitTest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStory_TabularModel_TabularModel",
                table: "UserStory");

            migrationBuilder.AddForeignKey(
                name: "FK_TabularModel_Workspace_Workspace",
                table: "TabularModel",
                column: "Workspace",
                principalTable: "Workspace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTest_UserStory_UserStory",
                table: "UnitTest",
                column: "UserStory",
                principalTable: "UserStory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStory_TabularModel_TabularModel",
                table: "UserStory",
                column: "TabularModel",
                principalTable: "TabularModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabularModel_Workspace_Workspace",
                table: "TabularModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitTest_UserStory_UserStory",
                table: "UnitTest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStory_TabularModel_TabularModel",
                table: "UserStory");

            migrationBuilder.AddForeignKey(
                name: "FK_TabularModel_Workspace_Workspace",
                table: "TabularModel",
                column: "Workspace",
                principalTable: "Workspace",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTest_UserStory_UserStory",
                table: "UnitTest",
                column: "UserStory",
                principalTable: "UserStory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStory_TabularModel_TabularModel",
                table: "UserStory",
                column: "TabularModel",
                principalTable: "TabularModel",
                principalColumn: "Id");
        }
    }
}
