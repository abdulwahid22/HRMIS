using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHCM.Persistence.Identity.Infrastructure
{
   public  class HCMIdentityFactory : IDesignTimeDbContextFactory<HCMIdentityDbContext>
    {
        public HCMIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HCMIdentityDbContext>();
            optionsBuilder.UseNpgsql("Server=localhost; Database =HCM; Username=postgres; Password=P@33word");
            return new HCMIdentityDbContext(optionsBuilder.Options);
        }
    }
    
}
