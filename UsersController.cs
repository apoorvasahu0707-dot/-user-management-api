using Microsoft.AspNetCore.Mvc;
using UserApi.Models;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // In-memory "database" for demo purposes
        private static readonly List<User> Users = new()
        {
            new User { Id = 1, Name = "Apoorva Sahu", Email = "apoorva@example.com", Age = 22 },
            new User { Id = 2, Name = "Rahul Verma", Email = "rahul@example.com", Age = 25 }
        };

        private static int _nextId = 3;

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(Users);
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = $"User with Id {id} not found." });
            }
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
            // Model validation runs automatically via [ApiController],
            // but we double check + return clean error messages
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Extra business-rule validation: no duplicate emails
            if (Users.Any(u => u.Email.Equals(newUser.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict(new { message = "A user with this email already exists." });
            }

            newUser.Id = _nextId++;
            Users.Add(newUser);

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        // PUT: api/users/1
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound(new { message = $"User with Id {id} not found." });
            }

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.Age = updatedUser.Age;

            return Ok(existingUser);
        }

        // DELETE: api/users/1
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = $"User with Id {id} not found." });
            }

            Users.Remove(user);
            return NoContent();
        }
    }
}
