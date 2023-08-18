using AutoMapper;
using BookService.DTO;
using BookService.Model;

namespace BookService.Mappers
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<CreateBookDTO, Book>();
            CreateMap<EditBookDTO, Book>()
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
