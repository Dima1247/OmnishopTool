using Microsoft.EntityFrameworkCore.Migrations;

namespace OmnishopFourSound.DAL.Migrations
{
    public partial class updatedproductcategoryextlinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OmnishopProductId",
                table: "tblProducts",
                newName: "OmnishopId");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "tblProductCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "tblProductCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "tblProductCategories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "tblProductCategories");

            migrationBuilder.RenameColumn(
                name: "OmnishopId",
                table: "tblProducts",
                newName: "OmnishopProductId");
        }
    }
}
