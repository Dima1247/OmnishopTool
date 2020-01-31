using Microsoft.EntityFrameworkCore.Migrations;

namespace OmnishopFourSound.DAL.Migrations
{
    public partial class fixedproductbrandconnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_tblBrands_OmnishopBrandId",
                table: "tblProducts");

            migrationBuilder.AlterColumn<int>(
                name: "OmnishopBrandId",
                table: "tblProducts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_tblBrands_OmnishopBrandId",
                table: "tblProducts",
                column: "OmnishopBrandId",
                principalTable: "tblBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProducts_tblBrands_OmnishopBrandId",
                table: "tblProducts");

            migrationBuilder.AlterColumn<int>(
                name: "OmnishopBrandId",
                table: "tblProducts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblProducts_tblBrands_OmnishopBrandId",
                table: "tblProducts",
                column: "OmnishopBrandId",
                principalTable: "tblBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
