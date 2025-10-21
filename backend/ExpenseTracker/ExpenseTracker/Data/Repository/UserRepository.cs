using ExpenseTracker.Entities;

namespace ExpenseTracker.Data.Repository
{
    public class UserRepository : ExpenseTrackerRepository<User>, IUserRepository
    {
        private readonly DatabaseContext _dbContext;
        public UserRepository(DatabaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<User>> GetUsersHasNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
