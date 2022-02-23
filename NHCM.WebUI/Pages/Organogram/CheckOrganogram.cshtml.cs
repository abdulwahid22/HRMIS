using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Lookup.Queries;
using NHCM.Application.Organogram.Models;
using NHCM.Application.Organogram.Queries;
using NHCM.Application.OrganogramCheckAndControl.Models;
using NHCM.Application.OrganogramCheckAndControl.Queries;
using NHCM.Domain.Entities;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.Pages.Ch
{
    public class CheckOrganogramModel : BasePage
    {
        public string SubScreens { get; set; } = "";
        public string Title { get; set; } = "adfasdfadf";

        private string htmltemplate = @"
                         <li><a href='#' data='$id' page='$path' class='sidebar-items' action='subscreen'><i class='$icon'></i>$title</a></li>
                                    ";


        public async Task OnGetAsync()
        {

            //Get Organization
            ListOfOrganization = new List<SelectListItem>();
            List<Organization> organizations = new List<Organization>();
            organizations = await Mediator.Send(new GetOrganiztionQuery() { Id = null });
            foreach (Organization organization in organizations)
                ListOfOrganization.Add(new SelectListItem(organization.Dari, organization.Id.ToString()));

            try
            {
                List<Screens> screens = new List<Screens>();
                screens = await Mediator.Send(new GetSubScreens() { ID = 19 });
                string listout = "";
                foreach (Screens s in screens)
                {
                    listout = listout + htmltemplate.Replace("$path", "dv_" + s.Path.Replace("/", "_")).Replace("$icon", s.Icon).Replace("$title", s.Title).Replace("$id", s.Id.ToString());
                }
                SubScreens = listout;
            }
            catch (Exception ex)
            {

            }

        }


        public async Task<IActionResult> OnPostSearch([FromBody] SearchOrganogramCheckQuery command)
        {

            try
            {
                List<SearchedCheckOrganogram> result = new List<SearchedCheckOrganogram>();
                result = await Mediator.Send(command);

                return new JsonResult(new NHCM.WebUI.Types.UIResult()
                {
                    Data = new { list = result },
                    Status = NHCM.WebUI.Types.UIStatus.Success,
                    Text = string.Empty,
                    Description = string.Empty

                });

            }
            catch (Exception ex)
            {
                return new JsonResult(new NHCM.WebUI.Types.UIResult()
                {

                    Data = null,
                    Status = NHCM.WebUI.Types.UIStatus.Failure,
                    Text = CustomMessages.InternalSystemException,

                    Description = ex.Message
                });
            }
        }
    }
}