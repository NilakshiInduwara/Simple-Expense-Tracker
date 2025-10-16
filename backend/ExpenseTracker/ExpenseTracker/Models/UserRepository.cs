using ExpenseTracker.Entities;

namespace ExpenseTracker.Models
{
    public static class UserRepository
    {
        public static List<User> Users { get; set; } = new List<User>()
        {
            new User
            {
                Id = 1,
                Name = "Test",
                Password = "Test pw",
                Email = "Test email",
            },
            new User
            {
                Id = 2,
                Name = "Test 2",
                Password = "Test pw 2",
                Email = "Test email 2",
            },
            new User
            {
                Id = 3,
                Name = "Test 3",
                Password = "Test pw",
                Email = "Test email 3",
            }
        };
    }
}
