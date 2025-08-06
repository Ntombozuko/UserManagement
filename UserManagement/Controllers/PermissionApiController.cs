using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PermissionApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }
    }
}
