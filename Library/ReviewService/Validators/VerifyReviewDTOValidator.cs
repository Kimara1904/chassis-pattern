using FluentValidation;
using ReviewService.DTOs;

namespace ReviewService.Validators
{
    public class VerifyReviewDTOValidator : AbstractValidator<VerifyReviewDTO>
    {
        public VerifyReviewDTOValidator()
        {
            RuleFor(x => x.VerifiedState).NotEmpty().NotEqual("Waiting").WithMessage("You can't put review on waiting")
                .Must(role => role.Equals("Accepted") || role.Equals("Denied")).WithMessage("VerifiedState must be Accepted or Denied."); ;
        }
    }
}
