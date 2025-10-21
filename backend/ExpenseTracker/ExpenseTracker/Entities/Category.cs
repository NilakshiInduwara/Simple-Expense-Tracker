namespace ExpenseTracker.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
