using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain;
using Users.Persistence;

namespace Users.Tests.Common
{
     public class Inno_ShopContextFactory
    {
        public static Guid UserIdForDelete = Guid.NewGuid();
        public static Guid UserIdForUpdate = Guid.NewGuid();
        public static Guid ProductIdForDelete = Guid.NewGuid();
        public static Guid ProductIdForUpdate = Guid.NewGuid();
        public static Inno_ShopDbContext Create()
        {
            var options = new DbContextOptionsBuilder<Inno_ShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new Inno_ShopDbContext(options);
            context.Database.EnsureCreated();
            context.Useres.AddRange(
                
                new UserSite
                {
                    Name = "user1",
                    Email = "example1@gmail.com",
                    Password = "1234Re_o",
                    Role = "User",
                    Id = Guid.Parse("12345678-1234-4567-1234-123456789012")
                },
                new UserSite
                {
                    Name = "user2",
                    Email = "example2@gmail.com",
                    Password = "513Rf0+2a",
                    Role = "User",
                    Id = Guid.Parse("12345678-1334-4567-1234-123456789012")
                },
                new UserSite
                {
                    Name = "user3",
                    Email = "example3@gmail.com",
                    Password = "06784DF3_Y",
                    Role = "User",
                    Id = UserIdForDelete
                },
                new UserSite
                {
                    Name = "user4",
                    Email = "example4@gmail.com",
                    Password = "1F_5Ju23",
                    Role = "User",
                    Id = UserIdForUpdate
                }
            );
            context.Products.AddRange(
                new Product
                {
                   Id = Guid.Parse("12345678-1334-4567-1234-123456789016"),
                   Name = "Product1",
                   Availabillity  = true,
                   DateCreating = Convert.ToDateTime("01.08.2024"),
                   Description = "Description1",
                   Price = 123.5M,
                   UserID = Guid.Parse("12345678-1234-4567-1234-123456789012")
                },
                 new Product
                 {
                     Id = Guid.Parse("12345678-1334-4567-1234-127456789016"),
                     Name = "Product2",
                     Availabillity = true,
                     DateCreating = Convert.ToDateTime("09.08.2024"),
                     Description = "Description2",
                     Price = 10M,
                     UserID = Guid.Parse("12345678-1234-4567-1234-123456789012")
                 },
                 new Product
                 {
                     Id = Guid.Parse("12345978-1334-4567-1234-127456789016"),
                     Name = "Product3",
                     Availabillity = false,
                     DateCreating = Convert.ToDateTime("02.08.2024"),
                     Description = "Description3",
                     Price = 345.9M,
                     UserID = Guid.Parse("12345678-1234-4567-1234-123456789012")
                 },
                 new Product
                 {
                     Id = ProductIdForUpdate,
                     Name = "Product1",
                     Availabillity = true,
                     DateCreating = Convert.ToDateTime("01.07.2024"),
                     Description = "Description1",
                     Price = 123.5M,
                     UserID = Guid.Parse("12345678-1334-4567-1234-123456789012")
                 },
                  new Product
                  {
                      Id = ProductIdForDelete,
                      Name = "Product2",
                      Availabillity = false,
                      DateCreating = Convert.ToDateTime("09.08.2024"),
                      Description = "Description1",
                      Price = 5.8M,
                      UserID = Guid.Parse("12345678-1334-4567-1234-123456789012")
                  }
                );
          context.SaveChanges();
            return context;
        }
        public static void Destroy(Inno_ShopDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
