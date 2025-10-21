using ExpenseTracker.Data.Config;
using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig()); 
            modelBuilder.ApplyConfiguration(new CategoryConfig());

        }
    }
}
