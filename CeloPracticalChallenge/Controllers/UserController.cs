using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CeloPracticalChallenge.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace CeloPracticalChallenge.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context; 

        private RandomUserGenerator _userGenerator = new RandomUserGenerator();

        public UserController(UserContext context)
        {
            _context = context;
        }

        // Get all users 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // Get user by id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserbyId(long id)
        {
            var User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        // Take the top {num} of users
        [HttpGet("limit/{num}")]
        public async Task<ActionResult<IEnumerable<User>>> GetNumOfUsers(int num)
        {
            return await _context.Users.Take(num).ToListAsync();
        }

        // Update user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, User userUpdate)
        {
            if (id != userUpdate.Id)
            {
                return BadRequest();
            }

            var User = await _context.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            User = userUpdate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UserExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> CreateUser(IEnumerable<User> users)
        {
            foreach (User user in users)
            {
                _context.Users.Add(user);
            } 
            await _context.SaveChangesAsync();

            return NoContent();
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id) =>
             _context.Users.Any(e => e.Id == id);
    }
}
