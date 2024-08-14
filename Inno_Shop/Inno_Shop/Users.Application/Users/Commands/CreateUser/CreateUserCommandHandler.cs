using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Interfaces;
using Users.Domain;

namespace Users.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler :IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserDbContext _dbContext;
        public CreateUserCommandHandler(IUserDbContext dbContext) => _dbContext = dbContext; 
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserSite
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Id = Guid.NewGuid(),
                Products = null,
                Role = null
        
            };
            await _dbContext.Useres.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
