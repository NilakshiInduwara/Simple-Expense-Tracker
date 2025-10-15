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
    }
}
