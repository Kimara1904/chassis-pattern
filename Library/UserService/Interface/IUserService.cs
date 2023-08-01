using UserService.DTOs;

namespace UserService.Interface
{
    public interface IUserService
    {
        Task<UserDTO> GetById(int id);
        Task<UserDTO> UpdateProfile(int idUser, EditUserDTO newInfo);
        Task ChangeRole(ChangeRoleDTO changeRoleDTO);
    }
}
