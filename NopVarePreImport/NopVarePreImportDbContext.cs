using Microsoft.EntityFrameworkCore;
using NopVarePreImport.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NopVarePreImport.DAL
{
    public class NopVarePreImportDbContext : DbContext
    {
        private const string connectionString = @"Server=.;Database=NopVarePreImport;Trusted_Connection=True;"; 
        public NopVarePreImportDbContext()
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<UProduct> UProducts { get; set; }

        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
