using Microsoft.EntityFrameworkCore.Migrations;

namespace OmnishopFourSound.DAL.Migrations
{
    public partial class addedomnishopcategoryparentalrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblCategories_ParentCategoryId",
                table: "tblCategories");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategories_ParentCategoryId",
                table: "tblCategories",
                column: "ParentCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblCategories_ParentCategoryId",
                table: "tblCategories");

            migrationBuilder.CreateIndex(
                name: "IX_tblCategories_ParentCategoryId",
                table: "tblCategories",
                column: "ParentCategoryId",
                unique: true,
                filter: "[ParentCategoryId] IS NOT NULL");
        }
    }
}
