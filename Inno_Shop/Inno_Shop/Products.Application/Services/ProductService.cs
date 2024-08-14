using Products.Application.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain;

namespace Products.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDbContext _context;

        public ProductService(IProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(ProductsFilterDto filter)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(p => p.Name.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter.UserName))
            {
                query = query.Where(p => p.User.Name.Contains(filter.UserName));
            }
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            if (filter.Availability.HasValue)
            {
                query = query.Where(p => p.Availabillity == filter.Availability.Value);
            }

            if (filter.MinDateCreating.HasValue)
            {
                query = query.Where(p => p.DateCreating >= filter.MinDateCreating.Value);
            }

            if (filter.MaxDateCreating.HasValue)
            {
                query = query.Where(p => p.DateCreating <= filter.MaxDateCreating.Value);
            }

            return await query.ToListAsync();
        }
    }
}
