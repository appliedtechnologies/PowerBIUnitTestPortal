using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class RemakeModel9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitTest_ResultType_ResultType",
                table: "UnitTest");

            migrationBuilder.DropTable(
                name: "ResultType");

            migrationBuilder.DropIndex(
                name: "IX_UnitTest_ResultType",
                table: "UnitTest");

            migrationBuilder.AlterColumn<string>(
                name: "ResultType",
                table: "UnitTest",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ResultType",
                table: "UnitTest",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ResultType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ResultType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Date" },
                    { 2, "Float" },
                    { 3, "Percentage" },
                    { 4, "String" }
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
                principalColumn: "Id");
        }
    }
}
