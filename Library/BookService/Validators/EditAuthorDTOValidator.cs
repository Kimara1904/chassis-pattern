using BookService.DTO;
using FluentValidation;

namespace BookService.Validators
{
    public class EditAuthorDTOValidator : AbstractValidator<EditAuthorDTO>
    {
        public EditAuthorDTOValidator()
        {
            RuleFor(a => a.FirstName).MaximumLength(20);
            RuleFor(a => a.LastName).MaximumLength(20);
        }
    }
}
