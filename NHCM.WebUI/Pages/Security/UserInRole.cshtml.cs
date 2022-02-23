using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NHCM.Persistence.Infrastructure.Identity;

namespace NHCM.WebUI.Pages.Security
{
    public class UserInRoleModel : PageModel
    {

        public string Message { get; set; }
        public List<SelectListItem> ListOfUsers = new List<SelectListItem>();
        public List<SelectListItem> ListOfRoles = new List<SelectListItem>();


       
        [BindProperty]
        [Required]
        public string RoleId { get; set; }

        [BindProperty]
        [Required]
        public string UserId { get; set; }

        

        private readonly RoleManager<HCMRole> _roleManager;
        private readonly UserManager<HCMUser> _userManager;

        public UserInRoleModel(UserManager<HCMUser> userManager, RoleManager<HCMRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public  void OnGet()
        {
            List<HCMUser> users =  _userManager.Users.ToList();
            foreach (HCMUser user in users)
                ListOfUsers.Add(new SelectListItem() { Text = user.UserName, Value = user.Id.ToString() });


            List<HCMRole> roles = new List<HCMRole>();
            roles =  _roleManager.Roles.ToList();
            foreach (HCMRole role in roles)
                ListOfRoles.Add(new SelectListItem() { Text = role.Name, Value = role.Id.ToString() });
         



        }

        public async Task<IActionResult> OnPostAddUserInRole()
        {

            if (ModelState.IsValid)
            {

                HCMUser user = await _userManager.FindByIdAsync(UserId);
                HCMRole role = await _roleManager.FindByIdAsync(RoleId);

                IdentityResult result = await _userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    Message = $" موفقانه به  {user.UserName} تعین شد {role.Name}";
                    return Page();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
               
            }
            else
            {
                return Page();
            }
        }
    }
}