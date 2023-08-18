using AutoMapper;
using BookService.DTO;
using BookService.Model;

namespace BookService.Mappers
{
    public class RentProfile : Profile
    {
        public RentProfile()
        {
            CreateMap<RentBookDTO, Rent>();
            CreateMap<Rent, RentDTO>();
        }
    }
}
