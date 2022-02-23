using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Employment.Models;
using NHCM.Application.Lookup.Queries;
using NHCM.Application.Reports.Models;
using NHCM.Application.Reports.Queries;
using NHCM.Domain.Entities;
using NHCM.Domain.ViewsEntities;
using NHCM.WebUI.Types;
using PersianLibrary;

namespace NHCM.WebUI.Pages.Reports
{
    public class AttendanceDailyReportModel : BasePage
    {
        //[BindProperty(SupportsGet = true)]
        //public string startdate { get; set; } = "test";


        //[BindProperty]
        //public DateTime enddate { get; set; }


        public async Task OnGetAsync()
        {
             

            ListOfDepartment = new List<SelectListItem>();
            List<Department> departments = new List<Department>();
            departments = await Mediator.Send(new GetDepartmentsQuery() { ID = null });
            foreach (Department department in departments)
                ListOfDepartment.Add(new SelectListItem() { Text = department.Name, Value = department.Id.ToString() });


            ListOfYears = new List<SelectListItem>();
            List<int> years = Enumerable.Range(PersianDate.Now.Year, 3).ToList();
           // List<object> yearslist = new List<object>();

            foreach (int i in years)
            {
                ListOfYears.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });

            }

            List<object> mlist = PersianDate.GetPersianMonths();
            ListOfMonths = new List<SelectListItem>();
            //ListOfMonths = mlist.Select(x => new SelectListItem() { Value = x.ToString(), Text = x.ToString()}).ToList();

            foreach (var item in mlist)
            {
                 
                int value = (int)item?.GetType().GetProperty("Value")?.GetValue(item, null);
                string text = (string)item?.GetType().GetProperty("Text")?.GetValue(item, null);

                ListOfMonths.Add(new SelectListItem() { Value = value.ToString(), Text = text.ToString() });
            }
        }

        public async Task<IActionResult> OnPostSearch([FromBody] SearchAttendanceReportQuery searchQuery)
        {
           
            try
            {
                List<MonthlyRecord> dbResult = new List<MonthlyRecord>();
                
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