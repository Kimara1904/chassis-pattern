using FluentValidation;
using UserService.DTOs;

namespace UserService.Validator
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(user => user.Username).NotEmpty().Length(6, 15);
            RuleFor(user => user.Email).EmailAddress().NotEmpty();
            RuleFor(user => user.Password).NotEmpty()
                      .Length(8, 30)
                      .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])")
                      .WithMessage("Password must contain at least one uppercase, lowercase letter, digit and special character");
            RuleFor(user => user.FirstName).NotEmpty().MaximumLength(30);
            RuleFor(user => user.LastName).NotEmpty().MaximumLength(30);
            RuleFor(user => user.Address).NotEmpty().MaximumLength(40);
        }
    }
}
