using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Lookup.Models;
using NHCM.Application.Lookup.Queries;
using NHCM.Application.Organogram.Commands;
using NHCM.Application.Organogram.Models;
using NHCM.Application.Organogram.Queries; 
using NHCM.Domain.Entities;
using NHCM.WebUI.Types;
using PersianLibrary;

namespace NHCM.WebUI.Pages.Employment
{
    public class SelectionApprovalModel : BasePage
    {

        public async Task OnGetAsync()
        {
            //Get Organization
            ListOfOrganization = new List<SelectListItem>();
            List<Organization> organizations = new List<Organization>();
            organizations = await Mediator.Send(new GetOrganiztionQuery() { Id = null });
            foreach (Organization organization in organizations)
                ListOfOrganization.Add(new SelectListItem(organization.Dari, organization.Id.ToString()));

            // Get Status
            ListOfStatus = new List<SelectListItem>();
            List<Status> statuses = new List<Status>();
            statuses = await Mediator.Send(new GetStatusQuery() { category = "OR" });
            foreach (Status status in statuses)
                ListOfStatus.Add(new SelectListItem() { Text = status.Dari, Value = status.Id.ToString() });


            List<int> years = Enumerable.Range(PersianDate.Now.Year - 1, 3).ToList();
            foreach (int i in years)
            {
                SelectListItem x = new SelectListItem();
                ListOfPersianYears.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }

            int ScreenID = Convert.ToInt32(EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]));
            List<SearchedProcess> Processes = await Mediator.Send(new GetProcess() { ScreenId = ScreenID });
            if (Processes.Any())
            {
                HttpContext.Session.SetInt32("ModuleID", Processes.FirstOrDefault().ModuleId);
                HttpContext.Session.SetInt32("ProcessID", Processes.FirstOrDefault().Id);
            }
        }

        

        public async Task<IActionResult> OnPostSearch([FromBody] SearchPlanQuery command)
        {
            try
            {
                List<SearchedPlan> result = new List<SearchedPlan>();
                result = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
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
                    Description = ex.Message
                });
            }
        }




        //public async Task OnGetAsync()
        //{
        //    //Get Organization
        //    ListOfOrganization = new List<SelectListItem>();
        //    List<Organization> organizations = new List<Organization>();
        //    organizations = await Mediator.Send(new GetOrganiztionQuery() { Id = null });
        //    foreach (Organization organization in organizations)
        //        ListOfOrganization.Add(new SelectListItem(organization.Dari, organization.Id.ToString()));

        //    List<int> years = Enumerable.Range(PersianDate.Now.Year - 1, 3).ToList();
        //    foreach (int i in years)
        //    {
        //        SelectListItem x = new SelectListItem();
        //        ListOfPersianYears.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
        //    }

        //}

        //public async Task<IActionResult> OnPostSearch([FromBody] SearchPlanQuery command)
        //{
        //    try
        //    {
        //        List<SearchedPlan> result = new List<SearchedPlan>();
        //        result = await Mediator.Send(command);
        //        return new JsonResult(new NHCM.WebUI.Types.UIResult()
        //        {
        //            Data = new { list = result },
        //            Status = NHCM.WebUI.Types.UIStatus.Success,
        //            Text = string.Empty,
        //            Description = string.Empty
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new NHCM.WebUI.Types.UIResult()
        //        {
        //            Data = null,
        //            Status = NHCM.WebUI.Types.UIStatus.Failure,
        //            Text = CustomMessages.InternalSystemException,
        //            Description = ex.Message
        //        });
        //    }
        //}
    }
}