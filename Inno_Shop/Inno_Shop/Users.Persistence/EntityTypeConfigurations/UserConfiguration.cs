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
   public class UserConfiguration : IEntityTypeConfiguration<UserSite>
    {
        public void Configure(EntityTypeBuilder<UserSite> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Id).IsUnique();
            builder.HasAlternateKey(user => new { user.Name, user.Email});
            builder.Property(user => user.Name).IsRequired();
            builder.Property(user => user.Name).HasMaxLength(256);
            builder.Property(user => user.Email).IsRequired();
            builder.Property(user => user.Password).IsRequired();
        }
    }
}
