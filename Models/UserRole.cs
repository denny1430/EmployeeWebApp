using System.Data;

namespace Employewebapp.Models
{
    public class UserRole
    {
        public int Id { get; set; }     // Primary Key
        public int UserId { get; set; }
        public int RoleId { get; set; }

        // Navigation properties (optional)
        public User User { get; set; }
        public Roles Role { get; set; }
    }
}
