using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class AddTabModel13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStory_Workspace_Workspace",
                table: "UserStory");

            migrationBuilder.RenameColumn(
                name: "Workspace ID",
                table: "Workspace",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Workspace",
                table: "UserStory",
                newName: "TabularModel");

            migrationBuilder.RenameColumn(
                name: "UserStory_ID",
                table: "UserStory",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserStory_Workspace",
                table: "UserStory",
                newName: "IX_UserStory_TabularModel");

            migrationBuilder.RenameColumn(
                name: "UnitTest_ID",
                table: "UnitTest",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "WorkspacePbId",
                table: "Workspace",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TabularModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", unicode: false, maxLength: 255, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetPbId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Workspace = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabularModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabularModel_Workspace_Workspace",
                        column: x => x.Workspace,
                        principalTable: "Workspace",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabularModel_Workspace",
                table: "TabularModel",
                column: "Workspace");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStory_TabularModel_TabularModel",
                table: "UserStory",
                column: "TabularModel",
                principalTable: "TabularModel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStory_TabularModel_TabularModel",
                table: "UserStory");

            migrationBuilder.DropTable(
                name: "TabularModel");

            migrationBuilder.DropColumn(
                name: "WorkspacePbId",
                table: "Workspace");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workspace",
                newName: "Workspace ID");

            migrationBuilder.RenameColumn(
                name: "TabularModel",
                table: "UserStory",
                newName: "Workspace");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserStory",
                newName: "UserStory_ID");

            migrationBuilder.RenameIndex(
                name: "IX_UserStory_TabularModel",
                table: "UserStory",
                newName: "IX_UserStory_Workspace");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UnitTest",
                newName: "UnitTest_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStory_Workspace_Workspace",
                table: "UserStory",
                column: "Workspace",
                principalTable: "Workspace",
                principalColumn: "Workspace ID");
        }
    }
}
