using Azure;
using ExpenseTracker.Entities;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.IdentityModel.Tokens;
using ExpenseTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly DatabaseContext _dbContext;

        public UsersController(ILogger<UsersController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        #region GetUsers
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

            _logger.LogInformation("Get Users Information");

            var users = _dbContext.Users.ToList();

            // if want to get specific data not all in the entity
            /*var users = _dbContext.Users.Select(user => new UserDTO()
            {
                Name = user.Name,
                Email = user.Email,
            }).ToList();*/
            return Ok(users);
        }
        #endregion

        #region GetUserById
        [HttpGet("{id:int}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            // BadRequest - 400
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
            
            var user = _dbContext.Users.Where(n => n.Id == id).FirstOrDefault();

            // NotFound- 404
            if (user == null)
            {
                _logger.LogError("User not found");
                return NotFound($"User with Id {id} not found.");
            }

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,

            };
            return Ok(userDTO);
        }
        #endregion

        #region GetUserByName
        [HttpGet("{name:alpha}", Name = "GetUserByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<User> GetUserByName(string name) { 
            if(name.IsNullOrEmpty()) return BadRequest();

            var user = _dbContext.Users.Where(n => n.Name == name).FirstOrDefault();

            if (user == null) return NotFound($"User with name {name} not found.");
            return Ok(user);
        }
        #endregion

        #region DeleteUserById
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id:int}", Name = "DeleteUserById")]
        public ActionResult<bool> DeleteUser(int id) {
            if (id <= 0) return BadRequest();

            var user = _dbContext.Users.Where(n => n.Id == id).FirstOrDefault();

            if (user == null) return NotFound($"User with Id {id} not found.");

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return Ok(true);
        }
        #endregion

        #region create
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDTO> CreateUser([FromBody] UserDTO model) { 
            if(model == null) return BadRequest();

            int newId = (_dbContext.Users.LastOrDefault()?.Id ?? 0) + 1;

            User user = new User() { 
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            model.Id = newId;

            return CreatedAtRoute("GetUserById", new { id = model.Id }, model);
        }
        #endregion

        #region update
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateUserById([FromBody] UserDTO userDTO) { 
            if(userDTO == null || userDTO.Id <=0) return BadRequest();

            var user = _dbContext.Users.AsNoTracking().Where(u => u.Id == userDTO.Id).FirstOrDefault();

            if(user == null) return NotFound();

            var newUser = new User()
            {
                Id = user.Id,
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password
            };
            _dbContext.Users.Update(newUser);

            /*user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Password = userDTO.Password;*/

            _dbContext.SaveChanges();

            return NoContent();
        }
        #endregion

        #region updatePartial
        //To update single property
        [HttpPatch]
        [Route("{id:int}/updatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateUserPartialById(int id, [FromBody] JsonPatchDocument<UserDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0) return BadRequest();

            var user = _dbContext.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user == null) return NotFound();

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };

            patchDocument.ApplyTo(userDTO, ModelState);

            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Password = userDTO.Password;

            _dbContext.SaveChanges();

            return NoContent();
        }
        #endregion

    }
}
