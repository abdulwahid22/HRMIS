using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHCM.Persistence.Infrastructure.Identity;

namespace NHCM.WebUI.API
{
    [AllowAnonymous]

    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
       
            private readonly SignInManager<HCMUser> _signInManger;
        private readonly UserManager<HCMUser> _userManager;

        public class person
        {

            public string username { get; set; }
            public string password { get; set; }
        }
        public LoginController(SignInManager<HCMUser> signInManager, UserManager<HCMUser> userManager)
        {
            _signInManger = signInManager;
            _userManager = userManager;
        }
            [HttpPost]
        public async Task<int> GetLogin([FromBody] person p)
        
            {
                int ret;
                var result =  await _signInManger.PasswordSignInAsync(p.username, p.password, true, true);

                if (result.Succeeded)
                {
                    ret = 1;
                }
                else
                {
                    ret = 0;
                }

            return ret;



            
        }

    }
}