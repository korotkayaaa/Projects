using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(updateUserCommand => updateUserCommand.Name).NotEmpty().WithMessage("Name is required.").Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
            RuleFor(updateUserCommand => updateUserCommand.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(8).WithMessage("Password must be at least 6 characters long.");
            RuleFor(updateUserCommand => updateUserCommand.Role).NotEmpty().WithMessage("Role is required.").Must(role => role == "Admin" || role == "User").WithMessage("Role must be either 'Admin' or 'User'.");
            RuleFor(updateUserCommand => updateUserCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
