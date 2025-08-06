using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.ViewModel;

namespace UserManagement.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAllUsers()
        {
            return await _context.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> PostUser(UserViewModel vm)
        {
            var user = new User
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            vm.Id = user.Id;
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, vm);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserViewModel vm)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        
        [HttpGet("count")]
        public async Task<int> GetUserCount()
        {
            return await _context.Users.CountAsync();
        }

        
        [HttpGet("group/{groupId}/count")]
        public async Task<int> GetUsersPerGroupCount(int groupId)
        {
            return await _context.UserGroups
                .Where(g => g.GroupId == groupId)
                .CountAsync();
        }
    }
}
