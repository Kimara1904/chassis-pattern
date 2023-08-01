using AutoMapper;
using Exceptions.Exeptions;
using Microsoft.AspNetCore.Identity;
using UserService.DTOs;
using UserService.Enums;
using UserService.Interface;
using UserService.Models;

namespace UserService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository repository, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task ChangeRole(ChangeRoleDTO changeRoleDTO)
        {
            var userQuery = await _repository.GetAllAsync();
            var user = userQuery.Where(u => u.Username.Equals(changeRoleDTO.UserUsername)).FirstOrDefault()
                ?? throw new NotFoundException(string.Format("There is no user with username: {0}", changeRoleDTO.UserUsername));
            user.Role = (UserRoles)Enum.Parse(typeof(UserRoles), changeRoleDTO.NewRole);
            await _repository.Save();
        }

        public async Task<UserDTO> GetById(int id)
        {
            var user = await _repository.FindAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateProfile(int idUser, EditUserDTO newInfo)
        {
            var users = await _repository.GetAllAsync();
            var user = users.Where(u => u.Id == idUser).FirstOrDefault() ?? throw new NotFoundException(string.Format("There is no user with id: {0}", idUser));

            if (newInfo.Email != null && newInfo.Email != user.Email)
            {
                var emailUser = users.Where(u => u.Email == newInfo.Email).FirstOrDefault();
                if (emailUser != null)
                {
                    throw new ConflictException(string.Format("User with email {0} already exists.", newInfo.Email));
                }
            }

            if (newInfo.Username != null && newInfo.Username != user.Username)
            {
                var usernameUser = users.Where(u => u.Username == newInfo.Username).FirstOrDefault();
                if (usernameUser != null)
                {
                    throw new ConflictException(string.Format("User with username {0} already exists.", newInfo.Username));
                }
            }

            _mapper.Map(newInfo, user);

            if (newInfo.NewPassword != null)
            {
                if (newInfo.OldPassword == null)
                    throw new BadRequestException("If you want to change password old password is required.");

                if (_passwordHasher.VerifyHashedPassword(user, user.Password, newInfo.OldPassword) != PasswordVerificationResult.Success)
                    throw new ConflictException("Wrong old password.");

                var result = _passwordHasher.HashPassword(user, newInfo.NewPassword);
                user.Password = result;
            }

            _repository.Update(user);
            await _repository.Save();

            return _mapper.Map<UserDTO>(user);
        }
    }
}
