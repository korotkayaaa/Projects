using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Users.Queries.GetUsersDetails
{
    public class GetUsersDetailsQuery : IRequest<UserDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
