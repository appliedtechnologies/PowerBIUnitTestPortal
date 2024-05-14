using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class RenameFloatToNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE dbo.UnitTest SET [ResultType] = 'Number' WHERE [ResultType] = 'Float'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE dbo.UnitTest SET [ResultType] = 'Float' WHERE [ResultType] = 'Number'");
        }
    }
}
