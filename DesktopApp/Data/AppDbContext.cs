using DesktopApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DesktopApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Party> Parties => Set<Party>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=billing.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Invoice → Product (One-to-Many)
            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Products)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Party → Invoice (One-to-Many)
            modelBuilder.Entity<Party>()
                .HasMany(p => p.Invoices)
                .WithOne(i => i.Party)
                .HasForeignKey(i => i.PartyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Optional: Seed sample data
            modelBuilder.Entity<Party>().HasData(
                new Party { Id = 1, Name = "Suresh Pharma", Address = "Trichy", Phone = "9876543210" },
                new Party { Id = 2, Name = "HealthCare Plus", Address = "Chennai", Phone = "1234567890" }
            );

            modelBuilder.Entity<Invoice>().HasData(
                new Invoice { InvoiceId = 1, PartyId = 1, InvoiceNo = "INV001", InvoiceDate = DateTime.Today },
                new Invoice { InvoiceId = 2, PartyId = 2, InvoiceNo = "INV002", InvoiceDate = DateTime.Today }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, InvoiceId = 1, ProductName = "Paracetamol", Quantity = 10, Price = 50 },
                new Product { ProductId = 2, InvoiceId = 1, ProductName = "Amoxicillin", Quantity = 5, Price = 100 },
                new Product { ProductId = 3, InvoiceId = 2, ProductName = "Cetrizine", Quantity = 20, Price = 25 }
            );
        }
    }
}
