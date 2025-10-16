using ExpenseTracker.Entities;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("id: int")]
        public ActionResult<User> GetUserById(int id)
        {
            // BadRequest - 400
            if (id <= 0) return BadRequest();
            
            var user = UserRepository.Users.Where(n => n.Id == id).FirstOrDefault();

            // NotFound- 404
            if (user == null) return NotFound($"User with Id {id} not found.");
            return Ok(user);
        }

        [HttpGet("{name:alpha}", Name = "GetUserByName")]
        public ActionResult<User> GetUserByName(string name) { 
            if(name.IsNullOrEmpty()) return BadRequest();

            var user = UserRepository.Users.Where(n => n.Name == name).FirstOrDefault();

            if (user == null) return NotFound($"User with name {name} not found.");
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:int}", Name = "DeleteUserById")]
        public ActionResult<bool> DeleteUser(int id) {
            if (id <= 0) return BadRequest();

            var user = UserRepository.Users.Where(n => n.Id == id).FirstOrDefault();

            if (user == null) return NotFound($"User with Id {id} not found.");

            UserRepository.Users.Remove(user);
            return Ok(true);
        }

    }
}
