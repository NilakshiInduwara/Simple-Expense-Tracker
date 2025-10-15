using ExpenseTracker.Data;
using ExpenseTracker.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public ExpensesController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Expense>>> GetAllExpenses()
        {
            var expenses = await _dbContext.Expenses.ToListAsync();
            /*var expenses = new List<Expense> {
                new Expense{
                    Id = 1,
                    ExpenseName = "Test",
                    Description = "Test Des",
                    ExpenseValue = 123,
                }
            };*/
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpenseById(int id)
        {
            var expense = await _dbContext.Expenses.FindAsync(id);
            
            if(expense == null) return NotFound("Expense not found.");

            return Ok(expense);
        }

        [HttpPost]
        public async Task<ActionResult<List<Expense>>> AddExpense(Expense expense)
        {
             _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Expenses.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Expense>>> UpdateExpense(Expense updatedExpense)
        {
            var expense = await _dbContext.Expenses.FindAsync(updatedExpense.Id);

            if (expense == null) return NotFound("Expense not found.");

            expense.ExpenseName = updatedExpense.ExpenseName;
            expense.Description = updatedExpense.Description;
            expense.ExpenseValue = updatedExpense.ExpenseValue;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Expenses.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Expense>> DeleteExpenseById(int id)
        {
            var expense = await _dbContext.Expenses.FindAsync(id);

            if (expense == null) return NotFound("Expense not found.");

            _dbContext.Expenses.Remove(expense);

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Expenses.ToListAsync());
        }
    }
}
