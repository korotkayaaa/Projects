using AutoMapper;
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

namespace Users.Application.Users.Queries.GetUsersDetails
{
    public class GetUsersDetailsQueryHandler : IRequestHandler<GetUsersDetailsQuery, UserDetailsVm>
    {
        private readonly IUserDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUsersDetailsQueryHandler(IUserDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper); 
        public async Task<UserDetailsVm> Handle(GetUsersDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Useres.FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);
            if(entity == null)
            {
                throw new NotFoundException(nameof(UserSite), request.Id);
            }
            return _mapper.Map<UserDetailsVm>(entity);
        }
    }
}
