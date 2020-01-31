using Microsoft.EntityFrameworkCore.Migrations;

namespace OmnishopFourSound.DAL.Migrations
{
    public partial class addedcategoryprops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "tblCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UrlPart",
                table: "tblCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "tblCategories");

            migrationBuilder.DropColumn(
                name: "UrlPart",
                table: "tblCategories");
        }
    }
}
