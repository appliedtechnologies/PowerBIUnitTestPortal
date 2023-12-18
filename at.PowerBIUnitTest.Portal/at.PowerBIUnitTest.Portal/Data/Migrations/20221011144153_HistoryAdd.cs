using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class HistoryAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", unicode: false, maxLength: 255, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Result = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    LastRun = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ExpectedRun = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    UnitTest = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_History_UnitTest_UnitTest",
                        column: x => x.UnitTest,
                        principalTable: "UnitTest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_History_UnitTest",
                table: "History",
                column: "UnitTest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");
        }
    }
}
