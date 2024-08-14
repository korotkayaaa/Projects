using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Users.Queries.GetUserList;
using Users.Persistence;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Queries
{
    [Collection("QueryCollection")]
    public class GetProductListQueryHandlerTest : TestCommandBase
    {
        private readonly Inno_ShopDbContext Context;
        private readonly IMapper Mapper;
        public GetProductListQueryHandlerTest(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }
        [Fact]
        public async Task GetUserListQueryHandler_Success()
        {
            var handler = new GetUserListQueryHandler(Context, Mapper);

            var result = await handler.Handle(
                new GetUserListQuery
                {

                }, CancellationToken.None);

            result.ShouldBeOfType<UserListVm>();
            result.Users.Count.ShouldBe(4);
        }
    }
}
