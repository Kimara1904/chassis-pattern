namespace UserService.DTOs
{
    public class ChangeRoleDTO
    {
        public string UserUsername { get; set; }
        public string NewRole { get; set; } = null!;
    }
}
