using DesktopApp.Model;
using Microsoft.EntityFrameworkCore;

namespace DesktopApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CompanyInfo> CompanyInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=K:\\Source Code\\DesktopApp\\DesktopApp\\billing.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define any table constraints or configuration here if needed in future
        }
    }
}
