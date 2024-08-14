using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Common.Exceptiosn;
using Users.Application.Interfaces;
using Users.Application.Users.Commands.ForgotPassword;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Commands
{
    public class ForgotPasswordCommandHandlerTest : TestCommandBase
    {
        private readonly ForgotPasswordCommandHandler _handler;
        private readonly Mock<IEmailService> _emailServiceMock;

        public ForgotPasswordCommandHandlerTest()
        {
            _emailServiceMock = new Mock<IEmailService>();
            _handler = new ForgotPasswordCommandHandler(Context, _emailServiceMock.Object);
        }

        [Fact]
        public async Task ForgotPasswordCommandHandler_Success()
        {
           

            var command = new ForgotPasswordCommand { Email = "example4@gmail.com" };

            await _handler.Handle(command, CancellationToken.None);

            var updatedUser = await Context.Useres.FindAsync(Inno_ShopContextFactory.UserIdForUpdate);
            Assert.NotNull(updatedUser.ResetToken);
            _emailServiceMock.Verify(email => email.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ForgotPasswordCommandHandler_ThrowsNotFoundException()
        {
            var command = new ForgotPasswordCommand { Email = "nonexistent@example.com" };

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
    
}
