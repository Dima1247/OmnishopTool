using Microsoft.EntityFrameworkCore.Migrations;

namespace NopVarePreImport.DAL.Migrations
{
    public partial class addedpathestocategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "tblCategory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "tblCategory");
        }
    }
}
