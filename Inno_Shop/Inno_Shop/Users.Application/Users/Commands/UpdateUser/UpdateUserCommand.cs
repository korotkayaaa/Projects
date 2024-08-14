using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
