namespace UserService.DTOs
{
    public class ChangeRoleDTO
    {
        public string UserUsername { get; set; } = null!;
        public string NewRole { get; set; } = null!;
    }
}
