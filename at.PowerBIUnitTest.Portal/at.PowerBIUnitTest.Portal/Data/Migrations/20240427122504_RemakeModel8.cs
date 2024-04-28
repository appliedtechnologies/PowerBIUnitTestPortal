using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Migrations
{
    public partial class RemakeModel8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Unique Identifier",
                table: "Workspace",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
                name: "Unique Identifier",
                table: "UserStory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
                name: "Unique Identifier",
                table: "UnitTest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
                name: "Unique Identifier",
                table: "TabularModel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unique Identifier",
                table: "Workspace");

            migrationBuilder.DropColumn(
                name: "Unique Identifier",
                table: "UserStory");

            migrationBuilder.DropColumn(
                name: "Unique Identifier",
                table: "UnitTest");

            migrationBuilder.DropColumn(
                name: "Unique Identifier",
                table: "TabularModel");
        }
    }
}
