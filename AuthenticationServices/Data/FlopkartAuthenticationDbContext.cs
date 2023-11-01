using AuthenticationServices.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServices.Data
{
    public class FlopkartAuthenticationDbContext:IdentityDbContext<AppUser>
    {
        public FlopkartAuthenticationDbContext(DbContextOptions<FlopkartAuthenticationDbContext> options):base(options)
        {

        }
    }
}
