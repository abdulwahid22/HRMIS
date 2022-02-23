using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NHCM.Persistence.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Identity.Infrastructure
{
   public  class HCMIdentityDbContext : IdentityDbContext<HCMUser, HCMRole, int, HCMUserClaims, HCMUserRole, HCMUserLogin, HCMRoleClaim, HCMUserToken>
    {
        public HCMIdentityDbContext(DbContextOptions<HCMIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseNpgsql("Server=localhost; Database =HCM; Username=postgres; Password=P@33word");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
