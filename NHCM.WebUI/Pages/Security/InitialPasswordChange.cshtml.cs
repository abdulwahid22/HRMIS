using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHCM.Persistence;
using NHCM.Persistence.Infrastructure.Identity;

namespace NHCM.WebUI.Pages.Security
{

    [Authorize(Policy = "FreshUserPolicy")]
    public class InitialPasswordChangeModel : PageModel
    {
        #region VM
        [BindProperty]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "رمز عبور فعلی ضروری میباشد")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

       
        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "رمز عبور ضروری میباشد")]
        [DataType(DataType.Password)]
        [BindProperty]
        public string NewPassword { get; set; }

        
        [Display(Name = "تکرار رمز عبور جدید")]
     //   [Compare("NewPassword", ErrorMessage = "رمز جدید و تاییدی آن مطابقت ندارد")]
        [Required(ErrorMessage ="لطفا رمز عبور خود را دوباره بنویسید")]
        [DataType(DataType.Password)]
        [BindProperty]
        public string NewPasswordConfirmation { get; set; }

        [BindProperty]
        public string Message { get; set; }
        #endregion VM
        #region DI
        private readonly UserManager<HCMUser> _userManager;
        private readonly SignInManager<HCMUser> _signInManager;
        private readonly HCMContext _context;
        public InitialPasswordChangeModel(UserManager<HCMUser> usermanager, SignInManager<HCMUser> signInManager, HCMContext context)
        {
            _userManager = usermanager;
            _signInManager = signInManager;
            _context = context;
        }
        #endregion DI


        public async Task<IActionResult> OnPostChangePassword()
        {
            HCMUser CurrentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            IdentityResult PaswordChangeResult = await _userManager.ChangePasswordAsync(CurrentUser, CurrentPassword, NewPassword);

            if (PaswordChangeResult.Succeeded)
            {
                CurrentUser.PasswordChanged = true;
                await _userManager.UpdateAsync(CurrentUser);
                Message = "کاربر عزیز رمز عبور شما موفقانه تعغیر یافت";

                return Page();

            }
            else
            {
                foreach (var error in PaswordChangeResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    Message += "\n " + error.Description;
                }
                return Page();
            }
            
            
   
        }
        public void OnGet()
        {

        }
    }
}