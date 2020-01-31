using Microsoft.EntityFrameworkCore.Migrations;

namespace NopVarePreImport.DAL.Migrations
{
    public partial class addednodeidtocategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NodeId",
                table: "tblCategory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NodeId",
                table: "tblCategory");
        }
    }
}
