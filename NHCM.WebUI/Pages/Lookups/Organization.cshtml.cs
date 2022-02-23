using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Lookup.Commands;
using NHCM.Application.Lookup.Models;
using NHCM.Application.Lookup.Queries;
using NHCM.Domain.Entities;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.Pages.Lookups
{
    //[Authorize(Policy = "SuperAdminPolicy")]
    public class OrganizationModel : BasePage
    {
        public async Task OnGetAsync()
        {
            ListOfOrganizationType = new List<SelectListItem>();
            List<OrganizationType> organizationTypes = new List<OrganizationType>();
            organizationTypes = await Mediator.Send(new GetOrganizationTypeQuery() { ID = null });
            foreach (OrganizationType organizationType in organizationTypes)
                ListOfOrganizationType.Add(new SelectListItem() { Text = organizationType.Name, Value = organizationType.Id.ToString() }); 
        }

        public async Task<IActionResult> OnPostSave([FromBody]SaveOrganiztionCommand command)
        {
            try
            {
                List<SearchedOrganizationModel> dbResult = await Mediator.Send(command);
               
                return new JsonResult(new UIResult()
                {
                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = "ارگان موفقانه ثبت سیستم شد",
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
            }
        }
         
        public async Task<IActionResult> OnPostSearch([FromBody] SearchOrganizationQuery searchQuery)
        {
            try
            {
                List<SearchedOrganizationModel> dbResult = new List<SearchedOrganizationModel>();
                dbResult = await Mediator.Send(searchQuery);

                return new JsonResult(new UIResult()
                {

                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = string.Empty,
                    Description = string.Empty

                }); 
            }
            catch (Exception ex)
            {
                return new JsonResult(new UIResult()
                {

                    Data = null,
                    Status = UIStatus.Failure,
                    Text = CustomMessages.InternalSystemException,
                    Description = ex.Message + " \n StackTrace : " + ex.StackTrace 
                });
            }
        }    
    }
}