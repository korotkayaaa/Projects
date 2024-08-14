using AutoMapper;
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

namespace Products.Application.Products.Queries.GetProductsDetails
{
    public class GetProductsDetailsQueryHandler : IRequestHandler<GetProductsDetailsQuery, ProductDetailsVm>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetProductsDetailsQueryHandler(IProductDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);
        public async Task<ProductDetailsVm> Handle(GetProductsDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);
            if (entity == null || entity.UserID!=request.UserId)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            return _mapper.Map<ProductDetailsVm>(entity);
        }
    }
}
