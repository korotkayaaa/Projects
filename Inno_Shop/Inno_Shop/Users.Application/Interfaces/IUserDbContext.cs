using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Users.Domain;
namespace Users.Application.Interfaces
{
    public interface IUserDbContext
    {
        DbSet<UserSite> Useres { get; set; }
        Task<UserSite> GetUserByEmailAsync(string email);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
         Task UpdateUserAsync(UserSite user);
    }
}
