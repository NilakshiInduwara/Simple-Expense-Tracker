using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseTracker.Data.Repository
{
    public class ExpenseTrackerRepository<T> : IExpenseTrackerRepository<T> where T : class
    {
        private readonly DatabaseContext _dbContext;
        private DbSet<T> _dbSet;
        public ExpenseTrackerRepository( DatabaseContext dbContext ) {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetFilteredDataAsync(Expression<Func<T, bool>> filter, bool useNoTracking)
        {
            if (useNoTracking)
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();

            return await _dbSet.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T dbRecord)
        {
            _dbSet.Add(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(T dbRecord)
        {
            _dbSet.Remove(dbRecord);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<T> UpdateAsync(T dbRecord)
        {
            _dbSet.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
        }
    }
}
