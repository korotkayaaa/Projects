using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Products.Application.Products.Commands.UpdateProduct;
using Users.Tests.Common;
using Xunit;
using Products.Application.Common.Exceptions;

namespace Users.Tests.Products.Commands
{
    public class UpdateProductCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task UpdateProductCommandHandler_Success()
        {
            var handler = new UpdateProductCommandHandler(Context);
            var updateName = "test name";

            await handler.Handle(new UpdateProductCommand
            {
                Id = Inno_ShopContextFactory.ProductIdForUpdate,
                Name = updateName,
                UserId = Guid.Parse("12345678-1334-4567-1234-123456789012")
            }, CancellationToken.None);

            Assert.NotNull(await Context.Products.SingleOrDefaultAsync(prod => prod.Id == Inno_ShopContextFactory.ProductIdForUpdate && prod.Name == updateName));
        }

        [Fact]
        public async Task UpdateProductCommandHandler_FailOnWrongId()
        {
            var handler = new UpdateProductCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateProductCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = Guid.Parse("12345678-1334-4567-1234-123456789012")

                    }, CancellationToken.None));
        }
        [Fact]
        public async Task UpdateProductCommandHandler_FailOnWrongUserId()
        {
            var handler = new UpdateProductCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                 await handler.Handle(
                     new UpdateProductCommand
                     {
                         Id = Inno_ShopContextFactory.ProductIdForUpdate,
                         UserId = Guid.Parse("12345678-1234-4567-1234-123456789012")
                     }, CancellationToken.None));
        }
    }
}

