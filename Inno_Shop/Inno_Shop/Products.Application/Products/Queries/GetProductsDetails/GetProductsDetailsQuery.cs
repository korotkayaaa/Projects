using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Products.Queries.GetProductsDetails
{
    public class GetProductsDetailsQuery : IRequest<ProductDetailsVm>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

    }
}
