using AutoMapper;
using ReviewService.DTOs;
using ReviewService.Model;

namespace ReviewService.Mapper
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDTO>();
            CreateMap<CreateReviewDTO, Review>();
            CreateMap<EditReviewDTO, Review>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                        return false;

                    if (srcMember is string)
                        return !string.IsNullOrEmpty((string)srcMember);

                    return true;
                })); ;
        }
    }
}
