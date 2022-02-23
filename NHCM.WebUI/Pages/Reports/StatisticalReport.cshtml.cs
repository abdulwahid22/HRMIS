using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Lookup.Queries;
using NHCM.Application.Reports.Queries;
using NHCM.Domain.Entities;
using NHCM.Domain.ReportEntities;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.Pages.Reports
{
    public class StatisticalReportModel : BasePage
    {
        public async Task OnGetAsync()
        {
            //Get Organization
            ListOfOrganization = new List<SelectListItem>();
            List<Organization> organizations = new List<Organization>();
            organizations = await Mediator.Send(new GetOrganiztionQuery() { Id = null });
            foreach (Organization organization in organizations)
                ListOfOrganization.Add(new SelectListItem(organization.Dari, organization.Id.ToString()));
        }

        public async Task<IActionResult> OnPostSearch([FromBody] SearchStatisticalReportQuery searchQuery)
        {
            try
            {
                List<StatisticalReport> dbResult = new List<StatisticalReport>();
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