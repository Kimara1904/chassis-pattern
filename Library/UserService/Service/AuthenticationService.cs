using AutoMapper;
using Exceptions.Exeptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.DTOs;
using UserService.Interface;
using UserService.Models;

namespace UserService.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserRepository repository, IMapper mapper, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _configuration = configuration;

        }

        public async Task<TokenDTO> Login(LoginDTO loginDTO)
        {
            var users = await _repository.GetAllAsync();
            User? user = users.Where(u => u.Email == loginDTO.Email && _passwordHasher.VerifyHashedPassword(u, u.Password, loginDTO.Password) == PasswordVerificationResult.Success).FirstOrDefault()
                ?? throw new NotFoundException(string.Format("User with email: {0} and password: {1} doesn't exists", loginDTO.Email, loginDTO.Password));

            return GetToken(user);
        }

        private TokenDTO GetToken(User user)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? "default"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Username", user.Username),
                        new Claim("Email", user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                        new Claim("Google", _configuration["Google:ClientId"] ?? "default")
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "default"));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn);

            return new TokenDTO() { Token = new JwtSecurityTokenHandler().WriteToken(token) };
        }

        public async Task Register(RegisterDTO newUser)
        {
            var users = await _repository.GetAllAsync();
            User? userEmail = users.Where(u => u.Email == newUser.Email).FirstOrDefault();

            if (userEmail != null)
            {
                throw new ConflictException(string.Format("User with email: {0} already exists.", newUser.Email));
            }

            User? userUsername = users.Where(u => u.Username == newUser.Username).FirstOrDefault();

            if (userUsername != null)
            {
                throw new ConflictException(string.Format("User with username: {0} already exists.", newUser.Username));
            }

            User user = _mapper.Map<User>(newUser);

            var result = _passwordHasher.HashPassword(user, newUser.Password);

            user.Password = result;

            user.Role = Enums.UserRoles.Customer;

            await _repository.Insert(user);
            await _repository.Save();
        }
    }
}
