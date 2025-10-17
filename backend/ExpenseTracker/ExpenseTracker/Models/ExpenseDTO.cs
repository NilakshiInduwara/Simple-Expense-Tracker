using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        [Required]
        public string ExpenseName { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Expense must be greater than zero.")]
        public decimal ExpenseValue { get; set; }
        public DateTime ExpenseDate { get; set; }
    }
}
