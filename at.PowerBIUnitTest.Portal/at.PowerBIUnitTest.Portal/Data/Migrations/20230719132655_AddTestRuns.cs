using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class AddTestRuns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestRuns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", unicode: false, maxLength: 255, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Result = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Workspace = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TabularModel = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Count = table.Column<int>(type: "int", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRuns", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestRuns");
        }
    }
}
