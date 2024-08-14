using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain;

namespace Products.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetFilteredProductsAsync(ProductsFilterDto filter);
    }
}
