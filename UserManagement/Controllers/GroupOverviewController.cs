using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;

namespace UserManagement.Controllers
{
    public class GroupOverviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupOverviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _context.Groups
                .Include(g => g.UserGroups)
                    .ThenInclude(ug => ug.User)
                .Include(g => g.GroupPermissions)
                    .ThenInclude(gp => gp.Permission)
                .ToListAsync();

            return View(groups);
        }
    }
}
