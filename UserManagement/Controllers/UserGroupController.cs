using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class UserGroupController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserGroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Assign(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var allGroups = await _context.Groups.ToListAsync();

            var userGroupIds = await _context.UserGroups
                .Where(ug => ug.UserId == userId)
                .Select(ug => ug.GroupId)
                .ToListAsync();

            ViewBag.User = user;
            ViewBag.Groups = allGroups;
            ViewBag.SelectedGroupIds = userGroupIds;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Assign(int userId, List<int> groupIds)
        {
            var existing = _context.UserGroups.Where(ug => ug.UserId == userId);
            _context.UserGroups.RemoveRange(existing);

            foreach (var gid in groupIds)
            {
                _context.UserGroups.Add(new UserGroup { UserId = userId, GroupId = gid });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Users");
        }

        
        public async Task<IActionResult> AssignUsers(int groupId)
        {
            var group = await _context.Groups.FindAsync(groupId);
            var allUsers = await _context.Users.ToListAsync();

            var selectedUserIds = await _context.UserGroups
                .Where(ug => ug.GroupId == groupId)
                .Select(ug => ug.UserId)
                .ToListAsync();

            ViewBag.Group = group;
            ViewBag.AllUsers = allUsers;
            ViewBag.SelectedUserIds = selectedUserIds;

            return View("AssignUsers"); // explicitly call the correct view
        }

        // POST: Save assigned users
        [HttpPost]
        public async Task<IActionResult> AssignUsers(int groupId, List<int> userIds)
        {
            var existing = _context.UserGroups.Where(ug => ug.GroupId == groupId);
            _context.UserGroups.RemoveRange(existing);

            foreach (var uid in userIds)
            {
                _context.UserGroups.Add(new UserGroup { GroupId = groupId, UserId = uid });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "GroupOverview");
        }

    }
}
