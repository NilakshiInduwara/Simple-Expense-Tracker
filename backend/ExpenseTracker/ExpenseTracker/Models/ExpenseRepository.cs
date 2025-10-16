using ExpenseTracker.Entities;

namespace ExpenseTracker.Models
{
    public static class ExpenseRepository
    {
        public static List<Expense> Expenses { get; set; } = new List<Expense>(){
                new Expense{
                    Id = 1,
                    ExpenseName = "Test",
                    Description = "Test Des",
                    ExpenseValue = 123,
                }
        };
    }
}
