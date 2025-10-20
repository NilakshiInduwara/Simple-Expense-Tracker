using Azure;
using ExpenseTracker.Entities;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.IdentityModel.Tokens;
using ExpenseTracker.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, DatabaseContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region GetUsers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> GetUsersAsync()
        {
            _logger.LogInformation("Get Users Information");

            #region if want to get specific data not all in the entity
            /*var users = await _dbContext.Users.Select(user => new UserDTO()
            {
                Name = user.Name,
                Email = user.Email,
            }).ToList();*/
            #endregion

            var users = await _dbContext.Users.ToListAsync();
            var usersDTOData = _mapper.Map<List<UserDTO>>(users);

            return Ok(usersDTOData);
        }
        #endregion

        #region GetUserById
        [HttpGet("{id:int}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> GetUserByIdAsync(int id)
        {
            // BadRequest - 400
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
            
            var user = await _dbContext.Users.Where(n => n.Id == id).FirstOrDefaultAsync();

            // NotFound- 404
            if (user == null)
            {
                _logger.LogError("User not found");
                return NotFound($"User with Id {id} not found.");
            }

            var userDTO = _mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }
        #endregion

        #region GetUserByName
        [HttpGet("{name:alpha}", Name = "GetUserByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetUserByNameAsync(string name) { 
            if(string.IsNullOrEmpty(name)) return BadRequest();

            var user = await _dbContext.Users.Where(n => n.Name == name).FirstOrDefaultAsync();

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
        public async Task<ActionResult<bool>> DeleteUserAsync(int id) {
            if (id <= 0) return BadRequest();

            var user = _dbContext.Users.Where(n => n.Id == id).FirstOrDefault();

            if (user == null) return NotFound($"User with Id {id} not found.");

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return Ok(true);
        }
        #endregion

        #region create
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDTO>> CreateUserAsync([FromBody] UserDTO model) { 
            if(model == null) return BadRequest();

            User user = _mapper.Map<User>(model);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            model.Id = user.Id;

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
        public async Task<ActionResult> UpdateUserByIdAsync([FromBody] UserDTO userDTO) { 
            if(userDTO == null || userDTO.Id <=0) return BadRequest();

            var user = await _dbContext.Users.AsNoTracking().Where(u => u.Id == userDTO.Id).FirstOrDefaultAsync();

            if(user == null) return NotFound();

            var newUser = _mapper.Map<User>(userDTO);
            _dbContext.Users.Update(newUser);

            /*user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.Password = userDTO.Password;*/

            await _dbContext.SaveChangesAsync();

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
        public async Task<ActionResult> UpdateUserPartialByIdAsync(int id, [FromBody] JsonPatchDocument<UserDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0) return BadRequest();

            var user = await _dbContext.Users.AsNoTracking().Where(u => u.Id == id).FirstOrDefaultAsync();

            if (user == null) return NotFound();

            var userDTO = _mapper.Map<UserDTO>(user);

            patchDocument.ApplyTo(userDTO, ModelState);

            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            user = _mapper.Map<User>(userDTO);

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        #endregion

    }
}
