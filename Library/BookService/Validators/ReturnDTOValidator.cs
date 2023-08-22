using BookService.DTO;
using FluentValidation;

namespace BookService.Validators
{
    public class ReturnDTOValidator : AbstractValidator<ReturnDTO>
    {
        public ReturnDTOValidator()
        {
            RuleFor(r => r.BookId).NotEmpty();
            RuleFor(r => r.Username).NotEmpty();
        }
    }
}
