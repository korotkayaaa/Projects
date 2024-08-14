using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Products.Application.Common.Exceptions;
using Products.Application.Products.Commands.DeleteProduct;
using Users.Tests.Common;
using Xunit;

namespace Users.Tests.Products.Commands
{
    public class DeleteProductCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task DeleteProductCommandHandler_Success()
        {
            var handler = new DeleteProductCommandHandler(Context);

            await handler.Handle(new DeleteProductCommand
            {
                Id = Inno_ShopContextFactory.ProductIdForDelete,
                UserId = Guid.Parse("12345678-1334-4567-1234-123456789012")

            }, CancellationToken.None);

            Assert.Null(Context.Products.SingleOrDefault(prod => prod.Id == Inno_ShopContextFactory.ProductIdForDelete));
        }
        [Fact]
        public async Task DeleteProductCommandHandler_FailOnWrongId()
        {
            var handler = new DeleteProductCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                 await handler.Handle(
                     new DeleteProductCommand
                     {
                         Id = Guid.NewGuid(),
                         UserId = Guid.Parse("12345678-1334-4567-1234-123456789012")
                     }, CancellationToken.None));
        }
        [Fact]
        public async Task DeleteProductCommandHandler_FailOnWrongUserId()
        {
            var handler = new DeleteProductCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                 await handler.Handle(
                     new DeleteProductCommand
                     {
                         Id = Inno_ShopContextFactory.ProductIdForDelete,
                         UserId = Guid.Parse("12345678-1234-4567-1234-123456789012")
                     }, CancellationToken.None));
        }
    }
}
