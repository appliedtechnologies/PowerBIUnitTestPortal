using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class AddResultType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResultType",
                table: "UnitTest",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResultType",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultType", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitTest_ResultType",
                table: "UnitTest",
                column: "ResultType");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTest_ResultType_ResultType",
                table: "UnitTest",
                column: "ResultType",
                principalTable: "ResultType",
                principalColumn: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitTest_ResultType_ResultType",
                table: "UnitTest");

            migrationBuilder.DropTable(
                name: "ResultType");

            migrationBuilder.DropIndex(
                name: "IX_UnitTest_ResultType",
                table: "UnitTest");

            migrationBuilder.DropColumn(
                name: "ResultType",
                table: "UnitTest");
        }
    }
}
