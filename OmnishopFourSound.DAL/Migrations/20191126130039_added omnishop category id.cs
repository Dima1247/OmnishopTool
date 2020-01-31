using Microsoft.EntityFrameworkCore.Migrations;

namespace OmnishopFourSound.DAL.Migrations
{
    public partial class addedomnishopcategoryid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OmnishopCategoryId",
                table: "tblCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OmnishopParentCategoryId",
                table: "tblCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OmnishopCategoryId",
                table: "tblCategories");

            migrationBuilder.DropColumn(
                name: "OmnishopParentCategoryId",
                table: "tblCategories");
        }
    }
}
