using Microsoft.EntityFrameworkCore.Migrations;

namespace OmnishopFourSound.DAL.Migrations
{
    public partial class addedomnishopbrandadditionalprops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OmnishopBrandId",
                table: "tblBrands",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UrlPart",
                table: "tblBrands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OmnishopBrandId",
                table: "tblBrands");

            migrationBuilder.DropColumn(
                name: "UrlPart",
                table: "tblBrands");
        }
    }
}
