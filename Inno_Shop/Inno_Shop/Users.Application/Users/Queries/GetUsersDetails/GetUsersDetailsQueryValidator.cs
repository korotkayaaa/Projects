using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Users.Queries.GetUsersDetails
{
     public class GetUsersDetailsQueryValidator : AbstractValidator<GetUsersDetailsQuery>
    {
        public GetUsersDetailsQueryValidator()
        {
            RuleFor(user => user.Id).NotEqual(Guid.Empty);
        }


    }
}
