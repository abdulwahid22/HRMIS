using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Lookup.Queries;
using NHCM.WebUI.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NHCM.Application.Lookup.Models;
using NHCM.Application.ProcessTracks.Queries;
using NHCM.Application.ProcessTracks.Models;
using NHCM.Application.ProcessTracks.Commands;

namespace NHCM.WebUI.Pages.Document
{

    public class ProcessModel : BasePage
    {
        private readonly IConfiguration _configuration;

        public ProcessModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            int ScreenID = Convert.ToInt32(EncryptionHelper.Decrypt(HttpContext.Request.Query["p"]));
            List<SearchedProcessConnection> ProcessConnections = await Mediator.Send(new GetProcessConnection() { });

            ProcessConnections = ProcessConnections.Where(c => c.ScreenId == ScreenID).ToList();

            List<SearchedProcess> Processes = await Mediator.Send(new GetProcess() { ScreenId = ScreenID });

            if (ProcessConnections.Any())
            {
                foreach (SearchedProcessConnection PC in ProcessConnections)
                    ListOfProcesses.Add(new SelectListItem(PC.ConnectionText, PC.ConnectionId.ToString()));
                if (Processes.Any())
                {
                    HttpContext.Session.SetInt32("ModuleID", Processes.FirstOrDefault().ModuleId);
                    HttpContext.Session.SetInt32("ProcessID", Processes.FirstOrDefault().Id);
                }
            }
        }


        public async Task<IActionResult> OnPostSave([FromBody] SaveProcessTracksCommand command)
        {
            try
            {
                List<SearchedProcessTracks> QueryResult = await Mediator.Send(new SearchProcessTrackQuery() { RecordId = command.RecordId, ModuleId = HttpContext.Session.GetInt32("ModuleID").Value });
                SearchedProcessTracks CurrentProcess = QueryResult.FirstOrDefault();

                if (command.Id == CurrentProcess.Id && CurrentProcess.ProcessId == HttpContext.Session.GetInt32("ProcessID"))
                {
                    command.ModuleId = CurrentProcess.ModuleId;
                    QueryResult = await Mediator.Send(command);
                    return new JsonResult(new UIResult()
                    {
                        Data = new { list = QueryResult },
                        Status = UIStatus.Success,
                        Text = "اسناد موفقانه ارسال گردید",
                        Description = "اسناد انتخاب شده، موفقانه به مرحله تعیین شده ارسال گردید"
                    });
                }
                else
                {
                    return new JsonResult(new UIResult()
                    {
                        Data = null,
                        Status = UIStatus.Failure,
                        Text = "کوشش خلاف اصول",
                        Description = "شما اجازه ارسال این سند را به مراحل انتخاب شده ندارید. سند مذکور خارج از حدود صلاحیت این مرحله میباشد"
                    });
                }
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


        public async Task<IActionResult> OnPostSearch([FromBody] SearchProcessTrackQuery command)
        {
            try
            {
                List<SearchedProcessTracks> QueryResult = new List<SearchedProcessTracks>();
                command.ModuleId = HttpContext.Session.GetInt32("ModuleID").Value;
                QueryResult = await Mediator.Send(command);
                return new JsonResult(new UIResult()
                {
                    Data = new { list = QueryResult },
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

        public IActionResult OnPostProcesses([FromBody] String ID)
        {
            return new JsonResult(new UIResult()
            {
                Data = 20,
                Status = UIStatus.Success,
                Text = string.Empty,
                Description = string.Empty
            });
        }

    }
}