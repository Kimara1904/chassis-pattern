using FluentValidation;
using UserService.DTOs;

namespace UserService.Validator
{
    public class EditUserDTOValidator : AbstractValidator<EditUserDTO>
    {
        public EditUserDTOValidator()
        {
            RuleFor(user => user.Username).Length(6, 15);
            RuleFor(user => user.Email).EmailAddress();
            RuleFor(user => user.NewPassword).MinimumLength(8)
                      .MaximumLength(30)
                      .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])")
                      .WithMessage("New password must contain at least one uppercase, lowercase letter, digit and special character");
            RuleFor(user => user.FirstName).MaximumLength(30);
            RuleFor(user => user.LastName).MaximumLength(30);
            RuleFor(user => user.Address).MaximumLength(40);
        }
    }
}
