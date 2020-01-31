using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NopVarePreImport.DAL.Migrations
{
    public partial class fixedcategoriesentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOnUTC",
                table: "tblUProduct",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "MenuTitle",
                table: "tblCategory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OmnishopCategoryId",
                table: "tblCategory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "tblCategory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuTitle",
                table: "tblCategory");

            migrationBuilder.DropColumn(
                name: "OmnishopCategoryId",
                table: "tblCategory");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "tblCategory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedOnUTC",
                table: "tblUProduct",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
