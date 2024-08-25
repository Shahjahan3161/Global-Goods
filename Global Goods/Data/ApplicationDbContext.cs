using Global_Goods.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Global_Goods.Data
{
    internal class ApplicationDbContext : DbContext // Inherit from DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Order_Detail> Order_Details { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-EPI50AN\\SQLEXPRESS;Database=dummy_data;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship between Order and Product
            modelBuilder.Entity<Order_Detail>()
                .HasKey(op => new { op.OrderID, op.ProductID });

            modelBuilder.Entity<Order_Detail>()
                .HasOne(op => op.Order)
                .WithMany(o => o.Order_Details)
                .HasForeignKey(op => op.OrderID);

            modelBuilder.Entity<Order_Detail>()
                .HasOne(op => op.Product)
                .WithMany(p => p.Order_Details)
                .HasForeignKey(op => op.ProductID);

            modelBuilder.Entity<Category>()
           .HasKey(c => c.CategoryID); // Primary key

            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryID)
                .ValueGeneratedOnAdd(); // Auto-increment
        }
    }
}
