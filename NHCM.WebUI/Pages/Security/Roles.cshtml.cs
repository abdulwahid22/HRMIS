using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHCM.Persistence.Infrastructure.Identity;

namespace NHCM.WebUI.Pages.Security
{
    public class RolesModel : PageModel
    {

        private readonly UserManager<HCMUser> _userManager;
        private readonly RoleManager<HCMRole> _roleManager;

        [MinLength(4, ErrorMessage = "نام رول حداقل باید دارای چهار حرف باشد")]
        [MaxLength(32, ErrorMessage = "نام رول حد اکثر میتواند دارای سی و دو حرف باشد")]
        [BindProperty]
        [Required]
        public string RoleName { get; set; }


        public string Message { get; set; }

        public RolesModel(UserManager<HCMUser> userManager, RoleManager<HCMRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void OnGet()
        {

        }


        public async Task<IActionResult> OnPostCreateRole()
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new HCMRole() { Name = RoleName });

                if (result.Succeeded)
                {
                    Message = "نقش موفقانه ثبت سیستم شد";
                    return Page();
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);

                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "درخواست شما درست نمیباشد");
                return Page();
            }
            return Page();

        }
    }
}