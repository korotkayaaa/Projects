using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Products.Queries.GetProductList.GetUserProductList
{
    public class GetProductListQuery : IRequest<ProductListVm>
    {
        public Guid UserId { get; set; }
    }
}
