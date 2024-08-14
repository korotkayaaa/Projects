using Microsoft.EntityFrameworkCore;
using Products.Application.Interfaces;
using Users.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain;
using Users.Persistence.EntityTypeConfigyrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Users.Persistence
{
    public class Inno_ShopDbContext:  IdentityDbContext<IdentityUser>, IUserDbContext, IProductDbContext
    {
        public DbSet<UserSite> Useres { get; set; }
        public DbSet<Product> Products { get; set; }
        public Inno_ShopDbContext(DbContextOptions<Inno_ShopDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);
        }
        public async Task<UserSite> GetUserByEmailAsync(string email)
        {
            return await Useres.SingleOrDefaultAsync(u => u.Email == email);
        }
        public async Task UpdateUserAsync(UserSite user)
        {
            Useres.Update(user);
            await SaveChangesAsync();
        }
    }
}
