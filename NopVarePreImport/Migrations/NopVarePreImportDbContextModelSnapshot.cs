﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NopVarePreImport.DAL;

namespace NopVarePreImport.DAL.Migrations
{
    [DbContext(typeof(NopVarePreImportDbContext))]
    partial class NopVarePreImportDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NopVarePreImport.DAL.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExtId");

                    b.Property<int>("Level");

                    b.Property<string>("MenuTitle");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NodeId");

                    b.Property<string>("OmnishopCategoryId");

                    b.Property<int?>("ParentCategoryId");

                    b.Property<string>("Path");

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("tblCategory");
                });

            modelBuilder.Entity("NopVarePreImport.DAL.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OmnishopProductId");

                    b.Property<int?>("UProductId");

                    b.HasKey("Id");

                    b.HasIndex("UProductId");

                    b.ToTable("tblProduct");
                });

            modelBuilder.Entity("NopVarePreImport.DAL.Entities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("tblProductCategory");
                });

            modelBuilder.Entity("NopVarePreImport.DAL.Entities.ProductSupplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<string>("ProductSupplierId");

                    b.Property<int>("SupplierId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SupplierId");

                    b.ToTable("tblProductSupplier");
                });

            modelBuilder.Entity("NopVarePreImport.DAL.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SupplierId");

                    b.Property<string>("SupplierName");

                    b.HasKey("Id");

                    b.ToTable("tblSupplier");
                });

            modelBuilder.Entity("NopVarePreImport.DAL.Entities.UProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContainerModel");

                    b.Property<string>("Currency");

                    b.Property<string>("DisplayName");

                    b.Property<string>("FullModel");

                    b.Property<string>("LongDescription");

                    b.Property<string>("ManufacturerProductId");

                    b.Property<DateTime?>("ModifiedOnUTC");

                    b.Property<double>("PackedWeight");

                    b.Property<string>("PimSKU");

                    b.Property<double>("Price");

                    b.Property<double>("PriceSuggestion");

                    b.Property<int?>("ProductId");

                    b.Property<string>("ProductStatusName");

                    b.Property<string>("ShortDescription");

                    b.Property<double>("SuggestedRetailPriceIncVat");

                    b.Property<string>("UProductId");

                    b.Property<int>("VAT");

                    b.Property<string>("VariantModel");

                    b.Property<string>("VendorProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("tblUProduct");
                });

            modelBuilder.Entity("NopVarePreImport.DAL.Entities.Category", b =>
                {
                    b.HasOne("NopVarePreImport.DAL.Entities.Category", "ParentCategory")
                        .WithMany("Categories")
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("NopVarePreImport.DAL.Entities.Product", b =>
                {
                    b.HasOne("NopVarePreImport.DAL.Entities.UProduct", "UProduct")
                        .WithMany()
                        .HasForeignKey("UProductId");
                });

            modelBuilder.Entity("NopVarePreImport.DAL.Entities.ProductCategory", b =>
                {
                    b.HasOne("NopVarePreImport.DAL.Entities.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NopVarePreImport.DAL.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NopVarePreImport.DAL.Entities.ProductSupplier", b =>
                {
                    b.HasOne("NopVarePreImport.DAL.Entities.Product", "Product")
                        .WithMany("ProductSuppliers")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NopVarePreImport.DAL.Entities.Supplier", "Supplier")
                        .WithMany("ProductSuppliers")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NopVarePreImport.DAL.Entities.UProduct", b =>
                {
                    b.HasOne("NopVarePreImport.DAL.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
