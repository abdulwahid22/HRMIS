using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Employment.Commands;
using NHCM.Application.Employment.Models;
using NHCM.Application.Employment.Queries;
using NHCM.Application.Lookup.Models;
using NHCM.Application.Lookup.Queries;
using NHCM.Application.ProcessTracks.Commands;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Queries;
using NHCM.Domain.Entities;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.Pages.Employment
{
    public class SelectionModel : BasePage
    {
        [BindProperty]
        public string ScreenID { get; set;  }

        public async Task OnGetAsync()
        {

            ListOfRanks = new List<SelectListItem>();
            List<Rank> ranks = new List<Rank>();
            ranks = await Mediator.Send(new GetRankQuery() { ID = null , CategoryId = 6});
            foreach (Rank rank in ranks)
                ListOfRanks.Add(new SelectListItem() { Text = rank.Name, Value = rank.Id.ToString() });


            ListOfDepartment = new List<SelectListItem>();
            List<Department> departments = new List<Department>();
            departments = await Mediator.Send(new GetDepartmentsQuery() { ID = null });
            foreach (Department department in departments)
                ListOfDepartment.Add(new SelectListItem() { Text = department.Name, Value = department.Id.ToString() });

            ScreenID = EncryptionHelper.Encrypt("51");

            int SID = Convert.ToInt32(EncryptionHelper.Decrypt(ScreenID));
          
            //int ScreenID = Convert.ToInt32(EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]));
            List<SearchedProcess> Processes = await Mediator.Send(new GetProcess() { ScreenId = SID });
            if (Processes.Any())
            {
                HttpContext.Session.SetInt32("ModuleID", Processes.FirstOrDefault().ModuleId);
                HttpContext.Session.SetInt32("ProcessID", Processes.FirstOrDefault().Id);
            }
        } 
        public async Task<IActionResult> OnPostEvents(string body)
        {
            try
            {
                List<EventType> EventTypes = new List<EventType>();
                EventTypes = await Mediator.Send(new GetEventTypeQuery() { ID = null });

                List<object> result = new List<object>();
                foreach (EventType e in EventTypes)
                    result.Add(new { Text = e.Name, ID = e.Id.ToString() });
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

                    Status =UIStatus.Failure,

                    Text = CustomMessages.InternalSystemException,
                    Description = ex.Message
                });
            }
        } 
        public async Task<IActionResult> OnPostEmployees(string body)
        {
            try
            {
                List<SearchedPersonModel> Persons = new List<SearchedPersonModel>();
                Persons = await Mediator.Send(new SearchPersonQuery() { Id = null, OrganizationId=2 });
                List<object> result = new List<object>();
                foreach (SearchedPersonModel p in Persons)
                    result.Add(new { Text = p.FirstName + "  "+ p.LastName + "  فرزند  " + p.FatherName, ID = p.Id.ToString() });
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
        public async Task<IActionResult> OnPostSave([FromBody]CreateSelectionCommand command)
        {
            try
            {
                List<SearchedSelectionModel> dbResult = new List<SearchedSelectionModel>();
                dbResult = await Mediator.Send(command); 
                if (dbResult.Any())
                {
                    int ModuleID = HttpContext.Session.GetInt32("ModuleID").Value;
                    int ProcessID = HttpContext.Session.GetInt32("ProcessID").Value;
                    int id = Convert.ToInt32(dbResult.FirstOrDefault().Id);

                    await Mediator.Send(new SaveProcessTracksCommand() { ModuleId = ModuleID, ProcessId = ProcessID, RecordId = id });
                } 
                return new JsonResult(new UIResult() 
                {
                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = "موفقانه ثبت گردید",
                    Description = "پیشنهاد تعیینات موفقانه در سیستم ثبت و راجستر گردید"
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
        public async Task<IActionResult> OnPostSearch([FromBody] SearchSelectionQuery command)
        {
            try
            {
                List<SearchedSelectionModel> result = new List<SearchedSelectionModel>();
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