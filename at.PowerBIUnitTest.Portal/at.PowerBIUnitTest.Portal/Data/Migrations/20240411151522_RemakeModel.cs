using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class RemakeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitTest_ResultType_ResultType",
                table: "UnitTest");

            migrationBuilder.Sql("UPDATE UnitTest SET ResultType = 1 WHERE ResultType = 'Date'");
            migrationBuilder.Sql("UPDATE UnitTest SET ResultType = 2 WHERE ResultType = 'Float'");
            migrationBuilder.Sql("UPDATE UnitTest SET ResultType = 3 WHERE ResultType = 'Percentage'");
            migrationBuilder.Sql("UPDATE UnitTest SET ResultType = 4 WHERE ResultType = 'String'");

            migrationBuilder.DropForeignKey(
                name: "FK_History_TestRuns_TestRun",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_History_UnitTest_UnitTest",
                table: "History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResultType",
                table: "ResultType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestRuns",
                table: "TestRuns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_History",
                table: "History");

            migrationBuilder.Sql("TRUNCATE TABLE History");
            migrationBuilder.Sql("TRUNCATE TABLE TestRuns");

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

            migrationBuilder.DropColumn(
                name: "LastResult",
                table: "UnitTest");

            migrationBuilder.DropColumn(
                name: "LastRun",
                table: "UnitTest");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "TestRuns");

            migrationBuilder.DropColumn(
                name: "TabularModel",
                table: "TestRuns");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "TestRuns");

            migrationBuilder.DropColumn(
                name: "UserStory",
                table: "TestRuns");

            migrationBuilder.DropColumn(
                name: "Workspace",
                table: "TestRuns");

            migrationBuilder.RenameTable(
                name: "TestRuns",
                newName: "TestRunCollection");

            migrationBuilder.RenameTable(
                name: "History",
                newName: "TestRun");

            migrationBuilder.RenameColumn(
                name: "WorkspacePbId",
                table: "Workspace",
                newName: "Ms Id");

            migrationBuilder.RenameColumn(
                name: "Beschreibung",
                table: "UserStory",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ExpectedResult",
                table: "UnitTest",
                newName: "Expected Result");

            migrationBuilder.RenameColumn(
                name: "DatasetPbId",
                table: "TabularModel",
                newName: "Ms Id");

            migrationBuilder.RenameColumn(
                name: "Result",
                table: "TestRun",
                newName: "WasPassed");

            migrationBuilder.RenameColumn(
                name: "ExpectedRun",
                table: "TestRun",
                newName: "Expected Result");

            migrationBuilder.RenameIndex(
                name: "IX_History_UnitTest",
                table: "TestRun",
                newName: "IX_TestRun_UnitTest");

            migrationBuilder.RenameIndex(
                name: "IX_History_TestRun",
                table: "TestRun",
                newName: "IX_TestRun_TestRun");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Workspace",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<Guid>(
                name: "Ms Id",
                table: "Workspace",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "TenantNavigationId",
                table: "Workspace",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UserStory",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "UserStory",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserStory",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "UserStory",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserStory",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "ResultType",
                table: "UnitTest",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UnitTest",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FloatSeparators",
                table: "UnitTest",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DecimalPlaces",
                table: "UnitTest",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DateTimeFormat",
                table: "UnitTest",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Expected Result",
                table: "UnitTest",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "UnitTest",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UnitTest",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "UnitTest",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UnitTest",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tenant",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TabularModel",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Ms Id",
                table: "TabularModel",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResultType",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ResultType",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "TestRunCollection",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "TestRunCollection",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "TestRunCollection",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "TestRunCollection",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "TestRunCollection",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStamp",
                table: "TestRun",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AlterColumn<int>(
                name: "TestRun",
                table: "TestRun",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastRun",
                table: "TestRun",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<bool>(
                name: "WasPassed",
                table: "TestRun",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Expected Result",
                table: "TestRun",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "TestRun",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "TestRun",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "TestRun",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "TestRun",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResultType",
                table: "ResultType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestRunCollection",
                table: "TestRunCollection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestRun",
                table: "TestRun",
                column: "Id");

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
                name: "IX_Workspace_TenantNavigationId",
                table: "Workspace",
                column: "TenantNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStory_CreatedBy",
                table: "UserStory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserStory_ModifiedBy",
                table: "UserStory",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTest_CreatedBy",
                table: "UnitTest",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTest_ModifiedBy",
                table: "UnitTest",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TestRunCollection_CreatedBy",
                table: "TestRunCollection",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TestRunCollection_ModifiedBy",
                table: "TestRunCollection",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TestRun_CreatedBy",
                table: "TestRun",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TestRun_ModifiedBy",
                table: "TestRun",
                column: "ModifiedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_TestRun_Created_By",
                table: "TestRun",
                column: "CreatedBy",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestRun_Modified_By",
                table: "TestRun",
                column: "ModifiedBy",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestRun_TestRunCollection_TestRun",
                table: "TestRun",
                column: "TestRun",
                principalTable: "TestRunCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestRun_UnitTest_UnitTest",
                table: "TestRun",
                column: "UnitTest",
                principalTable: "UnitTest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestRunCollection_Created_By",
                table: "TestRunCollection",
                column: "CreatedBy",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestRunCollection_Modified_By",
                table: "TestRunCollection",
                column: "ModifiedBy",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTest_Created_By",
                table: "UnitTest",
                column: "CreatedBy",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTest_Modified_By",
                table: "UnitTest",
                column: "ModifiedBy",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTest_ResultType_ResultType",
                table: "UnitTest",
                column: "ResultType",
                principalTable: "ResultType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStory_Created_By",
                table: "UserStory",
                column: "CreatedBy",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStory_Modified_By",
                table: "UserStory",
                column: "ModifiedBy",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workspace_Tenant_TenantNavigationId",
                table: "Workspace",
                column: "TenantNavigationId",
                principalTable: "Tenant",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestRun_Created_By",
                table: "TestRun");

            migrationBuilder.DropForeignKey(
                name: "FK_TestRun_Modified_By",
                table: "TestRun");

            migrationBuilder.DropForeignKey(
                name: "FK_TestRun_TestRunCollection_TestRun",
                table: "TestRun");

            migrationBuilder.DropForeignKey(
                name: "FK_TestRun_UnitTest_UnitTest",
                table: "TestRun");

            migrationBuilder.DropForeignKey(
                name: "FK_TestRunCollection_Created_By",
                table: "TestRunCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_TestRunCollection_Modified_By",
                table: "TestRunCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitTest_Created_By",
                table: "UnitTest");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitTest_Modified_By",
                table: "UnitTest");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitTest_ResultType_ResultType",
                table: "UnitTest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStory_Created_By",
                table: "UserStory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserStory_Modified_By",
                table: "UserStory");

            migrationBuilder.DropForeignKey(
                name: "FK_Workspace_Tenant_TenantNavigationId",
                table: "Workspace");

            migrationBuilder.DropIndex(
                name: "IX_Workspace_TenantNavigationId",
                table: "Workspace");

            migrationBuilder.DropIndex(
                name: "IX_UserStory_CreatedBy",
                table: "UserStory");

            migrationBuilder.DropIndex(
                name: "IX_UserStory_ModifiedBy",
                table: "UserStory");

            migrationBuilder.DropIndex(
                name: "IX_UnitTest_CreatedBy",
                table: "UnitTest");

            migrationBuilder.DropIndex(
                name: "IX_UnitTest_ModifiedBy",
                table: "UnitTest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResultType",
                table: "ResultType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestRunCollection",
                table: "TestRunCollection");

            migrationBuilder.DropIndex(
                name: "IX_TestRunCollection_CreatedBy",
                table: "TestRunCollection");

            migrationBuilder.DropIndex(
                name: "IX_TestRunCollection_ModifiedBy",
                table: "TestRunCollection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestRun",
                table: "TestRun");

            migrationBuilder.DropIndex(
                name: "IX_TestRun_CreatedBy",
                table: "TestRun");

            migrationBuilder.DropIndex(
                name: "IX_TestRun_ModifiedBy",
                table: "TestRun");

            migrationBuilder.DropColumn(
                name: "TenantNavigationId",
                table: "Workspace");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserStory");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserStory");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserStory");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserStory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UnitTest");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UnitTest");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UnitTest");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UnitTest");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ResultType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TestRunCollection");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TestRunCollection");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TestRunCollection");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "TestRunCollection");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TestRun");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TestRun");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TestRun");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "TestRun");

            migrationBuilder.RenameTable(
                name: "TestRunCollection",
                newName: "TestRuns");

            migrationBuilder.RenameTable(
                name: "TestRun",
                newName: "History");

            migrationBuilder.RenameColumn(
                name: "Ms Id",
                table: "Workspace",
                newName: "WorkspacePbId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "UserStory",
                newName: "Beschreibung");

            migrationBuilder.RenameColumn(
                name: "Expected Result",
                table: "UnitTest",
                newName: "ExpectedResult");

            migrationBuilder.RenameColumn(
                name: "Ms Id",
                table: "TabularModel",
                newName: "DatasetPbId");

            migrationBuilder.RenameColumn(
                name: "WasPassed",
                table: "History",
                newName: "Result");

            migrationBuilder.RenameColumn(
                name: "Expected Result",
                table: "History",
                newName: "ExpectedRun");

            migrationBuilder.RenameIndex(
                name: "IX_TestRun_UnitTest",
                table: "History",
                newName: "IX_History_UnitTest");

            migrationBuilder.RenameIndex(
                name: "IX_TestRun_TestRun",
                table: "History",
                newName: "IX_History_TestRun");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Workspace",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "WorkspacePbId",
                table: "Workspace",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Beschreibung",
                table: "UserStory",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "User",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "User",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ResultType",
                table: "UnitTest",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UnitTest",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FloatSeparators",
                table: "UnitTest",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DecimalPlaces",
                table: "UnitTest",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DateTimeFormat",
                table: "UnitTest",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpectedResult",
                table: "UnitTest",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "LastResult",
                table: "UnitTest",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastRun",
                table: "UnitTest",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                defaultValue: "Error");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tenant",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TabularModel",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DatasetPbId",
                table: "TabularModel",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResultType",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "TimeStamp",
                table: "TestRuns",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "TestRuns",
                type: "int",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TabularModel",
                table: "TestRuns",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "TestRuns",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserStory",
                table: "TestRuns",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Workspace",
                table: "TestRuns",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TimeStamp",
                table: "History",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "TestRun",
                table: "History",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LastRun",
                table: "History",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "History",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ExpectedRun",
                table: "History",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResultType",
                table: "ResultType",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestRuns",
                table: "TestRuns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_History",
                table: "History",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_History_TestRuns_TestRun",
                table: "History",
                column: "TestRun",
                principalTable: "TestRuns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_History_UnitTest_UnitTest",
                table: "History",
                column: "UnitTest",
                principalTable: "UnitTest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitTest_ResultType_ResultType",
                table: "UnitTest",
                column: "ResultType",
                principalTable: "ResultType",
                principalColumn: "Name");
        }
    }
}
