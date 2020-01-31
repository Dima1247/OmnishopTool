using Microsoft.EntityFrameworkCore.Migrations;

namespace OmnishopFourSound.DAL.Migrations
{
    public partial class addedwithoutnameprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WIthoutName",
                table: "tblProducts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WIthoutName",
                table: "tblProducts");
        }
    }
}
