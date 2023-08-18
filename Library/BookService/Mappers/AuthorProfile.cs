using AutoMapper;
using BookService.DTO;
using BookService.Model;

namespace BookService.Mappers
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDTO>();
            CreateMap<CreateAuthorDTO, Author>();
            CreateMap<EditAuthorDTO, Author>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                        return false;

                    if (srcMember is string)
                        return !string.IsNullOrEmpty((string)srcMember);

                    if (srcMember is int)
                        return (int)srcMember > 0;

                    return true;
                }));
        }
    }
}
