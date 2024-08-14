using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Tests.Common;
using Xunit;
using Products.Application.Products.Commands.CreateProduct;
using Products.Application.Common.Exceptions;

namespace Users.Tests.Products.Commands
{
     public class CreateProductCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task CreateProductCommandHandler_Success()
        {
            var handler = new CreateProductCommandHandler(Context, Context);
            var prodName = "product name";
            var prodDescription = "description test";
            var prodPrice = 19M;
            var prodAvailabillity = true;
            var userName = "user3";

            var prodId = await handler.Handle(
                new CreateProductCommand
                {
                    Name = prodName,
                    Description = prodDescription,
                    Price = prodPrice,
                    Availabillity = prodAvailabillity,
                    NameUser = userName
                },
                CancellationToken.None);

            Assert.NotNull(
                await Context.Products.SingleOrDefaultAsync(prod =>
                    prod.Description == prodDescription && prod.Id == prodId && prod.Name == prodName && prod.Price == prodPrice && prod.Availabillity == prodAvailabillity && prod.User.Name == userName));
        }
        [Fact]
    public async Task CreateProductCommandHandler_ThrowsNotFoundUserException()
        {
            var handler = new CreateProductCommandHandler(Context, Context);

            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                Availabillity = true,
                NameUser = "nonexistentuser"
            };

            await Assert.ThrowsAsync<NotFoundUserException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
