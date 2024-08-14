using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Users.Commands.CreateUser;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Users.Commands
{
     public class CreateUserCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task CreateUserCommandHandler_Success()
        {
            var handler = new CreateUserCommandHandler(Context);
            var userName = "user name";
            var userEmail = "email1@gmail.com";
            var password = "14R_ths2";

            var userId = await handler.Handle(
                new CreateUserCommand
                {
                    Name = userName,
                    Email = userEmail,
                    Password = password
                },
                CancellationToken.None);

            Assert.NotNull(
                await Context.Useres.SingleOrDefaultAsync(user =>
                    user.Email == userEmail && user.Id == userId && user.Name == userName && user.Password == password));
        }
    }
}
