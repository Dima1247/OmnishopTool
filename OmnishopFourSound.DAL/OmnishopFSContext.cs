using Microsoft.EntityFrameworkCore;
using OmnishopFourSound.DAL.Entities;
using System;

namespace OmnishopFourSound.DAL
{
    public class OmnishopFSContext : DbContext
    {
        private const string connectionString = @"Server=.;Database=OmnishopFourSoundDB;Trusted_Connection=True;";

        public OmnishopFSContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<entOmnishopProduct>()
                .HasOne(p => p.OmnishopBrand)
                .WithMany(b => b.OmnishopProducts)
                .HasForeignKey(p => p.OmnishopBrandId);

            modelBuilder.Entity<entOmnishopCategory>()
                .HasOne(c => c.ParentCategory)
                .WithMany(b => b.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId);

            modelBuilder.Entity<entOmnishopProductCategory>()
                .HasOne(p => p.OmnishopProduct)
                .WithMany(b => b.OmnishopProductCategories)
                .HasForeignKey(p => p.OmnishopProductId);

            modelBuilder.Entity<entOmnishopProductCategory>()
                .HasOne(p => p.OmnishopCategory)
                .WithMany(b => b.OmnishopProductCategories)
                .HasForeignKey(p => p.OmnishopCategoryId);

            modelBuilder.Entity<entOmnishopProductSpec>()
                .HasOne(p => p.OmnishopProduct)
                .WithMany(b => b.OmnishopProductSpecs)
                .HasForeignKey(p => p.OmnishopProductId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<entOmnishopProduct> Products { get; set; }
        public DbSet<entOmnishopBrand> Brands { get; set; }
        public DbSet<entOmnishopCategory> Categories { get; set; }
        public DbSet<entOmnishopProductCategory> ProductCategories { get; set; }
        public DbSet<entOmnishopProductSpec> ProductSpecs { get; set; }
    }
}
