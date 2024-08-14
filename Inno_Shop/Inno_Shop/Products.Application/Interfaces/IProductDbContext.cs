using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Users.Domain;
namespace Products.Application.Interfaces
{
    public interface IProductDbContext
    {
        DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
