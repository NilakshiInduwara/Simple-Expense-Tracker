namespace ExpenseTracker.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public required string ExpenseName { get; set; }
        public string? Description { get; set; }
        public required float ExpenseValue { get; set; }
    }
}
