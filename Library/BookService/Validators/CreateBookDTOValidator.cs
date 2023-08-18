using BookService.DTO;
using FluentValidation;

namespace BookService.Validators
{
    public class CreateBookDTOValidator : AbstractValidator<CreateBookDTO>
    {
        public CreateBookDTOValidator()
        {
            RuleFor(b => b.Title).NotEmpty().MaximumLength(30);
            RuleFor(b => b.Count).NotEmpty().GreaterThan(0);
        }
    }
}
