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
using Users.Application.Interfaces;
using Users.Domain;

namespace Products.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IUserDbContext _dbContextUser;

        public CreateProductCommandHandler(IProductDbContext dbContext, IUserDbContext dbContextUser) { _dbContext = dbContext; _dbContextUser = dbContextUser; }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContextUser.Useres.FirstOrDefaultAsync(user => user.Name == request.NameUser, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundUserException(request.NameUser);
            }
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Availabillity = request.Availabillity,
                UserID = entity.Id,
                User = entity,
                DateCreating = DateTime.Now            
            };
            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}
