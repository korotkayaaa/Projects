using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Common.Exceptiosn;
using Users.Application.Users.Commands.DeleteUser;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Commands
{
    public class DeleteProductCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task DeleteUserCommandHandler_Success()
        {
            var handler = new DeleteUserCommandHandler(Context);

            await handler.Handle(new DeleteUserCommand
            {
                Id = Inno_ShopContextFactory.UserIdForDelete,
            }, CancellationToken.None);

            Assert.Null(Context.Useres.SingleOrDefault(user => user.Id == Inno_ShopContextFactory.UserIdForDelete));
        }
        [Fact]
        public async Task DeleteUserCommandHandler_FailOnWrongId()
        {
            var handler = new DeleteUserCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                 await handler.Handle(
                     new DeleteUserCommand
                     {
                         Id = Guid.NewGuid()
                     }, CancellationToken.None));
        }
    }
}
