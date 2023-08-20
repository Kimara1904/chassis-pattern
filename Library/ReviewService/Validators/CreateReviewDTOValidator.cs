using FluentValidation;
using ReviewService.DTOs;

namespace ReviewService.Validators
{
    public class CreateReviewDTOValidator : AbstractValidator<CreateReviewDTO>
    {
        public CreateReviewDTOValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.Comment).NotEmpty();
            RuleFor(x => x.Rate).GreaterThanOrEqualTo(0);
        }
    }
}
