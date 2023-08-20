using FluentValidation;
using UserService.DTOs;

namespace UserService.Validator
{
    public class ChangeRoleDTOValidator : AbstractValidator<ChangeRoleDTO>
    {
        public ChangeRoleDTOValidator()
        {
            RuleFor(cr => cr.UserUsername).NotEmpty();
            RuleFor(cr => cr.NewRole).NotEmpty().NotEqual("Admin").WithMessage("Admin can't be registered")
                .Must(role => role.Equals("Librarian") || role.Equals("Customer")).WithMessage("Role must be Librarian or Customer.");
        }
    }
}
