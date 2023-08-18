using BookService.DTO;
using FluentValidation;

namespace BookService.Validators
{
    public class EditBookDTOValidator : AbstractValidator<EditBookDTO>
    {
        public EditBookDTOValidator()
        {
            RuleFor(b => b.Title).MaximumLength(30);
            RuleFor(b => b.Count).GreaterThan(0);
        }
    }
}
