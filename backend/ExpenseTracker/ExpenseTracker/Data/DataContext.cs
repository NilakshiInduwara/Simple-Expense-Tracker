using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ExpenseTracker.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        // Expenses - Table name
        public DbSet<Expense> Expenses { get; set; }
    }
}
