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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDTO> GetUsers()
        {
            /*var users = new List<UserDTO>();
            foreach(var user in UserRepository.Users)
            {
                UserDTO obj = new UserDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                };
                users.Add(obj);
            }*/

            var users = UserRepository.Users.Select(user => new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            });
            return Ok(users);
        }

        [HttpGet("{id:int}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<User> GetUserById(int id)
        {
            // BadRequest - 400
            if (id <= 0) return BadRequest();
            
            var user = UserRepository.Users.Where(n => n.Id == id).FirstOrDefault();

            // NotFound- 404
            if (user == null) return NotFound($"User with Id {id} not found.");

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,

            };
            return Ok(userDTO);
        }

        [HttpGet("{name:alpha}", Name = "GetUserByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<User> GetUserByName(string name) { 
            if(name.IsNullOrEmpty()) return BadRequest();

            var user = UserRepository.Users.Where(n => n.Name == name).FirstOrDefault();

            if (user == null) return NotFound($"User with name {name} not found.");
            return Ok(user);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id:int}", Name = "DeleteUserById")]
        public ActionResult<bool> DeleteUser(int id) {
            if (id <= 0) return BadRequest();

            var user = UserRepository.Users.Where(n => n.Id == id).FirstOrDefault();

            if (user == null) return NotFound($"User with Id {id} not found.");

            UserRepository.Users.Remove(user);
            return Ok(true);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDTO> CreateUser([FromBody] UserDTO model) { 
            if(model == null) return BadRequest();

            int newId = UserRepository.Users.LastOrDefault().Id + 1;

            User user = new User() { 
                Id = newId,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
            };

            UserRepository.Users.Add(user);

            model.Id = newId;

            return CreatedAtRoute("GetUserById", new { id = model.Id }, model);
        }

    }
}
