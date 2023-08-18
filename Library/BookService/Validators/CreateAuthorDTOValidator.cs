using BookService.DTO;
using FluentValidation;

namespace BookService.Validators
{
    public class CreateAuthorDTOValidator : AbstractValidator<CreateAuthorDTO>
    {
        public CreateAuthorDTOValidator()
        {
            RuleFor(a => a.FirstName).NotEmpty().MaximumLength(20);
            RuleFor(a => a.LastName).NotEmpty().MaximumLength(20);
        }
    }
}
