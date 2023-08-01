using UserService.DTOs;

namespace UserService.Interface
{
    public interface IAuthenticationService
    {
        Task Register(RegisterDTO newUser);
        Task<TokenDTO> Login(LoginDTO loginDTO);
    }
}
