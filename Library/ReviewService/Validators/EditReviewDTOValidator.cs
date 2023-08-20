using FluentValidation;
using ReviewService.DTOs;

namespace ReviewService.Validators
{
    public class EditReviewDTOValidator : AbstractValidator<EditReviewDTO>
    {
        public EditReviewDTOValidator()
        {
            RuleFor(x => x.Rate).GreaterThanOrEqualTo(0);
        }
    }
}
