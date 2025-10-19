using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options)
        {
            
        }
        DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new List<User>()
            {
                new User
                {
                    Id = 1,
                    Name = "Test Name",
                    Email = "Test Email",
                    Password = "Test PW",
                },
                new User
                {
                    Id = 2,
                    Name = "Test Name 2",
                    Email = "Test Email 2",
                    Password = "Test PW 2",
                }
            });
        }
    }
}
