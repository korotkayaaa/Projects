using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.WebApi.MiddleWare
{
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthentications(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
