using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NopVarePreImport.DAL.Migrations
{
    public partial class Initnewdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    ExtId = table.Column<int>(nullable: false),
                    ParentCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCategory_tblCategory_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "tblCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSupplier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SupplierId = table.Column<string>(nullable: true),
                    SupplierName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSupplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblProductCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductCategory_tblCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductSupplier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    ProductSupplierId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductSupplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProductSupplier_tblSupplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "tblSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UProductId = table.Column<string>(nullable: true),
                    PimSKU = table.Column<string>(nullable: true),
                    ContainerModel = table.Column<string>(nullable: true),
                    VariantModel = table.Column<string>(nullable: true),
                    FullModel = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    LongDescription = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    SuggestedRetailPriceIncVat = table.Column<double>(nullable: false),
                    VAT = table.Column<int>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    VendorProductId = table.Column<string>(nullable: true),
                    ManufacturerProductId = table.Column<string>(nullable: true),
                    ProductStatusName = table.Column<string>(nullable: true),
                    PriceSuggestion = table.Column<double>(nullable: false),
                    PackedWeight = table.Column<double>(nullable: false),
                    ModifiedOnUTC = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OmnishopProductId = table.Column<string>(nullable: true),
                    UProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProduct_tblUProduct_UProductId",
                        column: x => x.UProductId,
                        principalTable: "tblUProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCategory_ParentCategoryId",
                table: "tblCategory",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProduct_UProductId",
                table: "tblProduct",
                column: "UProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductCategory_CategoryId",
                table: "tblProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductCategory_ProductId",
                table: "tblProductCategory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductSupplier_ProductId",
                table: "tblProductSupplier",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductSupplier_SupplierId",
                table: "tblProductSupplier",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUProduct_ProductId",
                table: "tblUProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductCategory_tblProduct_ProductId",
                table: "tblProductCategory",
                column: "ProductId",
                principalTable: "tblProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblProductSupplier_tblProduct_ProductId",
                table: "tblProductSupplier",
                column: "ProductId",
                principalTable: "tblProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblUProduct_tblProduct_ProductId",
                table: "tblUProduct",
                column: "ProductId",
                principalTable: "tblProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblProduct_tblUProduct_UProductId",
                table: "tblProduct");

            migrationBuilder.DropTable(
                name: "tblProductCategory");

            migrationBuilder.DropTable(
                name: "tblProductSupplier");

            migrationBuilder.DropTable(
                name: "tblCategory");

            migrationBuilder.DropTable(
                name: "tblSupplier");

            migrationBuilder.DropTable(
                name: "tblUProduct");

            migrationBuilder.DropTable(
                name: "tblProduct");
        }
    }
}
