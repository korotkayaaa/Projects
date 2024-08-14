using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Products.Commands.UpdateProduct
{
    class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.").Length(10, 200).WithMessage("Description must be between 10 and 200 characters.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.Availabillity).NotNull().WithMessage("Availability must be specified.");
            RuleFor(x => x.DateCreating).NotEmpty().WithMessage("DateCreating is required.").LessThanOrEqualTo(DateTime.Now).WithMessage("DateCreating cannot be in the future.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        }
    }
}

