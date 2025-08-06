using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GroupApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        [HttpPost("{groupId}/permissions")]
        public async Task<IActionResult> AssignPermissionsToGroup(int groupId, List<int> permissionIds)
        {
            var group = await _context.Groups.FindAsync(groupId);
            if (group == null) return NotFound();

            var permissions = await _context.Permissions
                .Where(p => permissionIds.Contains(p.Id))
                .ToListAsync();

            foreach (var permission in permissions)
            {
                if (!_context.GroupPermissions.Any(gp => gp.GroupId == groupId && gp.PermissionId == permission.Id))
                {
                    _context.GroupPermissions.Add(new GroupPermission
                    {
                        GroupId = groupId,
                        PermissionId = permission.Id
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
