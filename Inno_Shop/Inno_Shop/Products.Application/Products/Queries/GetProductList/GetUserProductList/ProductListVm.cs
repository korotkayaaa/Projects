using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Products.Queries.GetProductList.GetAllProductList
{
    public class ProductListVm
    {
        public IList<ProductLookupDto> Products { get; set; }

    }
}
