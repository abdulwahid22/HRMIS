using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHCM.Persistence.Identity.Infrastructure;
using NHCM.Persistence.Infrastructure.Identity;

namespace NHCM.WebUI.Pages.Security
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<HCMUser> _signInManger;
        private readonly UserManager<HCMUser> _userManager;
              
        public LoginModel(SignInManager<HCMUser> signInManager, UserManager<HCMUser> userManager)
        {
            _signInManger = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public async void OnGetAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManger.PasswordSignInAsync(UserName, Password, false, false);

                if (result.Succeeded)
                {
                    if (!(await _userManager.FindByNameAsync(UserName)).PasswordChanged)
                        return LocalRedirect("~/Security/InitialPasswordChange");
                    else
                        return LocalRedirect("/index");
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "کاربر محترم حساب شما قفل شده است لطفا با بخش مدیر سیستم به تماس شوید");
                    }
                    ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه میباشد");
                    return Page();
                }
            }


            return Page();



        }
    }
}