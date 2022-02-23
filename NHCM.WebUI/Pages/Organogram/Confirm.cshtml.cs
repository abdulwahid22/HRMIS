using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHCM.Application.Organogram.Commands;
using NHCM.Application.Organogram.Models;
using NHCM.Application.Organogram.Queries;
using NHCM.WebUI.Types;

using MediatR;

namespace NHCM.WebUI.Pages.Organogram
{
    public class ConfirmModel : BasePage
    {
        //private IMediator _mediator;
        //protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        //private Mediator Mediator;
      

        public void OnGet()
        {

        }
       

        public async Task<IActionResult> OnPostSave([FromBody] ConfirmPlanCommand command)
        {

            try
            { 
                // change status id to dynamic id
                command.StatusId = 52;

                List<SearchedPlan> dbResult = new List<SearchedPlan>();
                dbResult = await Mediator.Send(command);

                return new JsonResult(new NHCM.WebUI.Types.UIResult()
                {
                    Data = new { list = dbResult },
                    Status = NHCM.WebUI.Types.UIStatus.Success,
                    Text = "تشکیل موفقانه ارسال شد",
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
                    Description = ex.Message + " \n StackTrace : " + ex.StackTrace

                });
            }
        }
    }
}