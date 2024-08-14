using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<IdentityUser> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<IdentityUser>();
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
  }
