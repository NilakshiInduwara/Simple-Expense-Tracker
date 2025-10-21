using ExpenseTracker.Entities;

namespace ExpenseTracker.Data.Repository
{
    public interface IUserRepository : IExpenseTrackerRepository<User>
    {
        Task<List<User>> GetUsersHasNameAsync(string name);
    }
}
