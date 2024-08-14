using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Interfaces;
using System;

namespace Users.Application.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler
        : IRequestHandler<GetUserListQuery, UserListVm>
    {
        private readonly IUserDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(IUserDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<UserListVm> Handle(GetUserListQuery request,
            CancellationToken cancellationToken)
        {
            var usersQuery = await _dbContext.Useres
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new UserListVm { Users = usersQuery };
        }

        public Task Handle(GetUserListQuery getUserListQuery, object cancelletionToken)
        {
            throw new NotImplementedException();
        }
    }
}