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
using NHCM.Application.ProcessTracks.Commands;
using NHCM.Application.ProcessTracks.Models;
using NHCM.Application.ProcessTracks.Queries;
using NHCM.Domain.Entities;
using NHCM.WebUI.Types;
using PersianLibrary;

namespace NHCM.WebUI.Pages.Organogram
{



  //  [Authorize(Policy = "OrganizationTahskil")]
    public class PlanModel : BasePage
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

        public async Task<IActionResult> OnPostSave([FromBody]SavePlanCommand command)
        {
            try
            {
                List<SearchedPlan> dbResult = await Mediator.Send(command);
                if (dbResult.Any())
                {
                    int ModuleID = HttpContext.Session.GetInt32("ModuleID").Value;
                    int ProcessID = HttpContext.Session.GetInt32("ProcessID").Value;                    
                    await Mediator.Send(new SaveProcessTracksCommand() { ModuleId = ModuleID, ProcessId = ProcessID, RecordId = dbResult.FirstOrDefault().Id });
                }

                return new JsonResult(new UIResult()
                {
                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = "تشکیل موفقانه ثبت سیستم شد",
                    Description = string.Empty
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(CustomMessages.FabricateException(ex));
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
    }
}