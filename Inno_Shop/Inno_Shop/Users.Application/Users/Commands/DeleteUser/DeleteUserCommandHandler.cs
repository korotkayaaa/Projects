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

namespace Users.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserDbContext _dbContext;
        public DeleteUserCommandHandler(IUserDbContext dbContext) => _dbContext = dbContext;
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Useres.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(UserSite), request.Id);
            }
            _dbContext.Useres.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
