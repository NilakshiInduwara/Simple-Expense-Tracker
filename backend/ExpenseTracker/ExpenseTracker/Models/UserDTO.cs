using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class UserDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the user name")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a valid password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-zA-Z)(?=.*\d)(?=.*[@#!$%^&*()+-_=?<>.]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and include letters, numbers, and symbols."
        )]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Please enter the email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public required string Email { get; set; }
    }
}
