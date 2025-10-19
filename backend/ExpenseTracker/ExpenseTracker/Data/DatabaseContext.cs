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
    }
}
