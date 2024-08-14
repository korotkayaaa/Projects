using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Common.Exceptiosn;
using Users.Application.Users.Commands.UpdateUser;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Commands
{
    public class UpdateProductCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task UpdateUserCommandHandler_Success()
        {
            var handler = new UpdateUserCommandHandler(Context);
            var updatePassword = "123Er_56J";

            await handler.Handle(new UpdateUserCommand
            {
                Id = Inno_ShopContextFactory.UserIdForUpdate,
                Password = updatePassword
            }, CancellationToken.None);

            Assert.NotNull(await Context.Useres.SingleOrDefaultAsync(user => user.Id == Inno_ShopContextFactory.UserIdForUpdate && user.Password == updatePassword));
        }

        [Fact]
        public async Task UpdateUserCommandHandler_FailOnWrongId()
        {
            var handler = new UpdateUserCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateUserCommand
                    {
                        Id = Guid.NewGuid()
                    }, CancellationToken.None));
        }
    }
}

