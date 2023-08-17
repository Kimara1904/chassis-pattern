using AutoMapper;
using UserService.DTOs;
using UserService.Models;

namespace UserService.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<EditUserDTO, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                        return false;

                    if (srcMember is string)
                        return !string.IsNullOrEmpty((string)srcMember);

                    return true;
                }));
            CreateMap<RegisterDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
