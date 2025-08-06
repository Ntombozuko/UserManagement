using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
    public class User
    {

        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName}{LastName}";

        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    }
}
