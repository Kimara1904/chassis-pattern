using UserService.Enums;

namespace UserService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public UserRoles Role { get; set; }
        public bool Deleted { get; set; }
    }
}
