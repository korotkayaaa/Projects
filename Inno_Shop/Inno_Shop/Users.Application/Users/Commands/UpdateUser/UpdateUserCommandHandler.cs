using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Common.Exceptiosn;
using Users.Application.Interfaces;
using Users.Domain;

namespace Users.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserDbContext _dbContext;
        public UpdateUserCommandHandler(IUserDbContext dbContext) => _dbContext = dbContext; 
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Useres.FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);
            if(entity == null)
            {
                throw new NotFoundException(nameof(UserSite), request.Id);
            }
            if (request.Name is not null) entity.Name = request.Name;
            if(request.Password is not null) entity.Password = request.Password;
            if (request.Role is not null) entity.Role = request.Role;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
