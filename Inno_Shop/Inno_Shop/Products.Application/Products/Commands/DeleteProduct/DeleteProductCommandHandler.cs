using MediatR;
using Products.Application.Common.Exceptions;
using Products.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Domain;

namespace Products.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductDbContext _dbContext;
        public DeleteProductCommandHandler(IProductDbContext dbContext) => _dbContext = dbContext;
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            if (entity.UserID == request.UserId)
            {
                _dbContext.Products.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            } else { throw new NotFoundException(nameof(Product), request.Id); }
            
            return Unit.Value;

        }
    }
}
