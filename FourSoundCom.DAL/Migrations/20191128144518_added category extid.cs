using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FourSoundCom.DAL.Migrations
{
    public partial class addedcategoryextid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
              name: "ExtId",
              table: "tblCategory",
              nullable: false,
              defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtId",
                table: "tblCategory");
        }
    }
}
