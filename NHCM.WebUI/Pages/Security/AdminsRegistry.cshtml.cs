using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NHCM.WebUI.Pages.Security
{

    [Authorize("SuperAdmin")]
    public class AdminsRegistryModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}