using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain;
namespace Users.Persistence.EntityTypeConfigyrations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);
            builder.HasIndex(product => product.Id).IsUnique();
            builder.HasOne(product => product.User).WithMany(user => user.Products).HasForeignKey(product => product.UserID);
            builder.Property(product => product.Name).IsRequired();
            builder.Property(product => product.Price).IsRequired();
            builder.Property(product => product.Availabillity).IsRequired();
            builder.Property(product => product.DateCreating).IsRequired();

        }
    }
}

