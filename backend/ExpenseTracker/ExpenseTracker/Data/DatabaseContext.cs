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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig()); 

        }
    }
}
