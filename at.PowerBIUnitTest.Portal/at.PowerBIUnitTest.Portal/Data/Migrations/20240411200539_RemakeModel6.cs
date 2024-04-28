using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class RemakeModel6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workspace_Tenant_TenantNavigationId",
                table: "Workspace");

            migrationBuilder.RenameColumn(
                name: "TenantNavigationId",
                table: "Workspace",
                newName: "Tenant");

            migrationBuilder.RenameIndex(
                name: "IX_Workspace_TenantNavigationId",
                table: "Workspace",
                newName: "IX_Workspace_Tenant");

            migrationBuilder.AddForeignKey(
                name: "FK_Workspace_Tenant",
                table: "Workspace",
                column: "Tenant",
                principalTable: "Tenant",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workspace_Tenant",
                table: "Workspace");

            migrationBuilder.RenameColumn(
                name: "Tenant",
                table: "Workspace",
                newName: "TenantNavigationId");

            migrationBuilder.RenameIndex(
                name: "IX_Workspace_Tenant",
                table: "Workspace",
                newName: "IX_Workspace_TenantNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workspace_Tenant_TenantNavigationId",
                table: "Workspace",
                column: "TenantNavigationId",
                principalTable: "Tenant",
                principalColumn: "Id");
        }
    }
}
