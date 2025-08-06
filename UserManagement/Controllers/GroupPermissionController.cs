using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class GroupPermissionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupPermissionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Assign(int groupId)
        {
            var group = await _context.Groups.FindAsync(groupId);
            var permissions = await _context.Permissions.ToListAsync();

            var selectedIds = await _context.GroupPermissions
                .Where(gp => gp.GroupId == groupId)
                .Select(gp => gp.PermissionId)
                .ToListAsync();

            ViewBag.Group = group;
            ViewBag.Permissions = permissions;
            ViewBag.SelectedPermissionIds = selectedIds;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Assign(int groupId, List<int> permissionIds)
        {
            var existing = _context.GroupPermissions.Where(gp => gp.GroupId == groupId);
            _context.GroupPermissions.RemoveRange(existing);

            foreach (var pid in permissionIds)
            {
                _context.GroupPermissions.Add(new GroupPermission
                {
                    GroupId = groupId,
                    PermissionId = pid
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "GroupOverview");
        }
    }
}
