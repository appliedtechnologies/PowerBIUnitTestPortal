using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class UpdateResultType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ResultType",
                column: "Name",
                values: new object[]
                {
                    "Date",
                    "Float",
                    "Percentage",
                    "String"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ResultType",
                keyColumn: "Name",
                keyValue: "Date");

            migrationBuilder.DeleteData(
                table: "ResultType",
                keyColumn: "Name",
                keyValue: "Float");

            migrationBuilder.DeleteData(
                table: "ResultType",
                keyColumn: "Name",
                keyValue: "Percentage");

            migrationBuilder.DeleteData(
                table: "ResultType",
                keyColumn: "Name",
                keyValue: "String");
        }
    }
}
