using Microsoft.EntityFrameworkCore;
using System;

namespace DesktopApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize()
        {
            using var context = new AppDbContext();

            // Drop and recreate the DB — WARNING: this erases data
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Seed dummy/test data here if needed
            // context.Products.Add(new Product { Name = "Sample", Price = 100, Quantity = 5 });
            // context.SaveChanges();
        }
    }
}
