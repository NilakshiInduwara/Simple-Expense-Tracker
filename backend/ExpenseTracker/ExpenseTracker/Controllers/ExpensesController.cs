using ExpenseTracker.Entities;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Expense> GetAllExpenses()
        {
            return Ok(ExpenseRepository.Expenses);
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<ExpenseDTO> CreateExpense([FromBody] ExpenseDTO expenseDTO)
        {
            if (expenseDTO == null) return BadRequest();

            Expense expense = new Expense
            {
                ExpenseName = expenseDTO.ExpenseName,
                Description = expenseDTO.Description,
                ExpenseValue = expenseDTO.ExpenseValue,
                ExpenseDate = expenseDTO.ExpenseDate,
            };

            ExpenseRepository.Expenses.Add(expense);
            return Ok(expenseDTO);
        }
    }
}
