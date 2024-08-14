using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Users.Queries.GetUsersDetails;
using Users.Persistence;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Queries
{
    [Collection("QueryCollection")]
    public class GetUserDetailsQueryHandlerTest : TestCommandBase
    {
        private readonly Inno_ShopDbContext Context;
        private readonly IMapper Mapper;
        public GetUserDetailsQueryHandlerTest(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }
        [Fact]
        public async Task GetUserDetailsQueryHandler_Success()
        {
            var handler = new GetUsersDetailsQueryHandler(Context, Mapper);

            var result = await handler.Handle(
                new GetUsersDetailsQuery
                {
                    Id = Guid.Parse("12345678-1334-4567-1234-123456789012")

                }, CancellationToken.None);

            result.ShouldBeOfType<UserDetailsVm>();
            result.Name.ShouldBe("user2");
            result.Email.ShouldBe("example2@gmail.com");
        }
    }
}
