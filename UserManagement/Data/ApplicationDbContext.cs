using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId });

            modelBuilder.Entity<GroupPermission>()
                .HasKey(gp => new { gp.GroupId, gp.PermissionId });

            modelBuilder.Entity<Group>().HasData(
               new Group { Id = 1, Name = "Admins" },
               new Group { Id = 2, Name = "Editors" },
               new Group { Id = 3, Name = "Viewers" }
           );

            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, Name = "Level 1" },
                new Permission { Id = 2, Name = "Level 2" },
                new Permission { Id = 3, Name = "Level 3" }
            );

        }
    }
}
