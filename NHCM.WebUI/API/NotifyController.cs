using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHCM.Domain.Entities;
using NHCM.Persistence;

namespace NHCM.WebUI.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
    

        private readonly HCMContext _context;

        public NotifyController(HCMContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<int> getNotify([FromBody] IdentityCard ch)
        {
            HCMContext _db = new HCMContext();
            List<IdentityCard> results = (from p in _db.IdentityCard
                                          where p.PersonId.Equals(ch.PersonId)
                                          select p).ToList();

            foreach (IdentityCard p in results)
            {
               
                p.StatusID = 1;
               
                
            }

            _db.SaveChanges();
            return 1;
        }

        }
}
