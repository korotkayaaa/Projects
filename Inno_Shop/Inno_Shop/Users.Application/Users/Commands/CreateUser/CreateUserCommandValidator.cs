using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Users.Commands.CreateUser
{
     public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(createUserCommand => createUserCommand.Name).NotEmpty().WithMessage("Name is required.").Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
            RuleFor(createUserCommand => createUserCommand.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid email format.");
            RuleFor(createUserCommand => createUserCommand.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(8).WithMessage("Password must be at least 6 characters long.");

        }
    }
}
