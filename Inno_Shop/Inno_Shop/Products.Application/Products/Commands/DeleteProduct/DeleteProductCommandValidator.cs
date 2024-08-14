using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Products.Commands.DeleteProduct
{
    class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        }
    }
}
