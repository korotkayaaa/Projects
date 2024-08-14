using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Application.Common.Exceptions;
using Products.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Domain;

namespace Products.Application.Products.Commands.UpdateProduct
{
     public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductDbContext _dbContext;
        public UpdateProductCommandHandler(IProductDbContext dbContext) => _dbContext = dbContext;
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            if (entity.UserID == request.UserId)
            {
                if (request.Name is not null) entity.Name = request.Name;
                if (request.Description is not null) entity.Description = request.Description;
                if (request.Availabillity.HasValue)  entity.Availabillity = (bool)request.Availabillity;
                if (request.Price.HasValue) entity.Price = (decimal)request.Price;
                if (request.Price.HasValue) entity.DateCreating = (DateTime)request.DateCreating;
                await _dbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            } else { throw new NotFoundException(nameof(Product), request.Id); }
        }
    }
}