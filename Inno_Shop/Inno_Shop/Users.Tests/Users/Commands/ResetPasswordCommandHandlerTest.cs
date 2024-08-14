using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Users.Commands.ResetPassword;
using Users.Domain;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Commands
{
    public class ResetPasswordCommandHandlerTest : TestCommandBase
    {
        private readonly ResetPasswordCommandHandler _handler;

        public ResetPasswordCommandHandlerTest()
        {
            _handler = new ResetPasswordCommandHandler(Context);
        }

        [Fact]
        public async Task ResetPasswordCommandHandler_Success()
        {
            var user = new UserSite { Email = "test@example.com", ResetToken = "valid-token", Name = "user5", Password = "dfr$5h6", Id = Guid.NewGuid() };
            Context.Useres.Add(user);
            await Context.SaveChangesAsync();

            var command = new ResetPasswordCommand { Email = "test@example.com", Token = "valid-token", NewPassword = "new-password" };

            await _handler.Handle(command, CancellationToken.None);

            var updatedUser = await Context.Useres.FindAsync(user.Id);
            Assert.Equal("new-password", updatedUser.Password);
            Assert.Null(updatedUser.ResetToken);
        }

        [Fact]
        public async Task ResetPasswordCommandHandler_ThrowsUnauthorizedAccessException()
        {
            var user = new UserSite { Email = "test@example.com", ResetToken = "valid-token", Name = "user5", Password = "dfr$5h6", Id = Guid.NewGuid() };
            Context.Useres.Add(user);
            await Context.SaveChangesAsync();

            var command = new ResetPasswordCommand { Email = "test@example.com", Token = "invalid-token", NewPassword = "new-password" };

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ResetPasswordCommandHandler_UserDoesNotExist()
        {
            var command = new ResetPasswordCommand { Email = "nonexistent@example.com", Token = "some-token", NewPassword = "new-password" };

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
