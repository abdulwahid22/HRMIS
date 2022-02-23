using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NHCM.Persistence.Infrastructure.Identity;


namespace NHCM.WebUI.Pages.Security
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<HCMUser> _signInManager;
       // private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<HCMUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
         //   _logger = logger;
        }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            ///  _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }

        //public async Task<IActionResult> OnPost(string returnUrl = null)
        //{
           
        //}
    }
}