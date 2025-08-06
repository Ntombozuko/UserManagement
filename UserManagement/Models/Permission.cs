namespace UserManagement.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
    }
}
