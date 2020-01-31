﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OmnishopFourSound.DAL;

namespace OmnishopFourSound.DAL.Migrations
{
    [DbContext(typeof(OmnishopFSContext))]
    [Migration("20191126130331_added omnishop category type")]
    partial class addedomnishopcategorytype
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OmnishopFourSound.DAL.Entities.entOmnishopBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("tblBrands");
                });

            modelBuilder.Entity("OmnishopFourSound.DAL.Entities.entOmnishopCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Level");

                    b.Property<string>("Name");

                    b.Property<int>("OmnishopCategoryId");

                    b.Property<int>("OmnishopParentCategoryId");

                    b.Property<int?>("ParentCategoryId");

                    b.Property<int>("SortOrder");

                    b.Property<string>("UrlPart");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId")
                        .IsUnique()
                        .HasFilter("[ParentCategoryId] IS NOT NULL");

                    b.ToTable("tblCategories");
                });

            modelBuilder.Entity("OmnishopFourSound.DAL.Entities.entOmnishopProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cost");

                    b.Property<string>("LongDescription");

                    b.Property<string>("Name");

                    b.Property<int>("OmnishopBrandId");

                    b.Property<string>("OmnishopId");

                    b.Property<string>("Picture1");

                    b.Property<string>("Picture2");

                    b.Property<string>("Picture3");

                    b.Property<double>("Price");

                    b.Property<bool>("Published");

                    b.Property<string>("ShortDescription");

                    b.Property<int>("UComProductId");

                    b.Property<int>("Vat");

                    b.HasKey("Id");

                    b.HasIndex("OmnishopBrandId");

                    b.ToTable("tblProducts");
                });

            modelBuilder.Entity("OmnishopFourSound.DAL.Entities.entOmnishopProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OmnishopCategoryId");

                    b.Property<int>("OmnishopProductId");

                    b.HasKey("Id");

                    b.HasIndex("OmnishopCategoryId");

                    b.HasIndex("OmnishopProductId");

                    b.ToTable("tblProductCategories");
                });

            modelBuilder.Entity("OmnishopFourSound.DAL.Entities.entOmnishopCategory", b =>
                {
                    b.HasOne("OmnishopFourSound.DAL.Entities.entOmnishopCategory", "ParentCategory")
                        .WithOne()
                        .HasForeignKey("OmnishopFourSound.DAL.Entities.entOmnishopCategory", "ParentCategoryId");
                });

            modelBuilder.Entity("OmnishopFourSound.DAL.Entities.entOmnishopProduct", b =>
                {
                    b.HasOne("OmnishopFourSound.DAL.Entities.entOmnishopBrand", "OmnishopBrand")
                        .WithMany("OmnishopProducts")
                        .HasForeignKey("OmnishopBrandId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OmnishopFourSound.DAL.Entities.entOmnishopProductCategory", b =>
                {
                    b.HasOne("OmnishopFourSound.DAL.Entities.entOmnishopCategory", "OmnishopCategory")
                        .WithMany("OmnishopProductCategories")
                        .HasForeignKey("OmnishopCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OmnishopFourSound.DAL.Entities.entOmnishopProduct", "OmnishopProduct")
                        .WithMany("OmnishopProductCategories")
                        .HasForeignKey("OmnishopProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
