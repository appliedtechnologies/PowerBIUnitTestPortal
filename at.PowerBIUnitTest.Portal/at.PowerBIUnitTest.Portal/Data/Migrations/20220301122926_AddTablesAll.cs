using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class AddTablesAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Workspace",
                table: "Workspace");

            migrationBuilder.AddColumn<int>(
                name: "Workspace ID",
                table: "Workspace",
                type: "int",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workspace",
                table: "Workspace",
                column: "Workspace ID");

            migrationBuilder.CreateTable(
                name: "UserStory",
                columns: table => new
                {
                    UserStory_ID = table.Column<int>(type: "int", unicode: false, maxLength: 255, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Beschreibung = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Workspace = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStory", x => x.UserStory_ID);
                    table.ForeignKey(
                        name: "FK_UserStory_Workspace_Workspace",
                        column: x => x.Workspace,
                        principalTable: "Workspace",
                        principalColumn: "Workspace ID");
                });

            migrationBuilder.CreateTable(
                name: "UnitTest",
                columns: table => new
                {
                    UnitTest_ID = table.Column<int>(type: "int", unicode: false, maxLength: 255, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Ergebnis = table.Column<bool>(type: "bit", unicode: false, maxLength: 255, nullable: false),
                    UserStory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTest", x => x.UnitTest_ID);
                    table.ForeignKey(
                        name: "FK_UnitTest_UserStory_UserStory",
                        column: x => x.UserStory,
                        principalTable: "UserStory",
                        principalColumn: "UserStory_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitTest_UserStory",
                table: "UnitTest",
                column: "UserStory");

            migrationBuilder.CreateIndex(
                name: "IX_UserStory_Workspace",
                table: "UserStory",
                column: "Workspace");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitTest");

            migrationBuilder.DropTable(
                name: "UserStory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workspace",
                table: "Workspace");

            migrationBuilder.DropColumn(
                name: "Workspace ID",
                table: "Workspace");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workspace",
                table: "Workspace",
                column: "Name");
        }
    }
}
