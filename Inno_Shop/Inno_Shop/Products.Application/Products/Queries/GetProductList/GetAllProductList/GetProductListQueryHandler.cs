using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Products.Application.Products.Queries.GetProductList.GetAllProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListVm>
    {
        private readonly IProductDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetProductListQueryHandler(IProductDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);
        public async Task<ProductListVm> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var usersQuery = await _dbContext.Products
                .Where(product => product.Name != null)
                .ProjectTo<ProductLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new ProductListVm { Products = usersQuery };
        }
    }
}
