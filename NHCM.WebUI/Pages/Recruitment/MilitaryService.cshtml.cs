using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NHCM.Application.Lookup.Queries;
using NHCM.Application.Recruitment.Commands;
using NHCM.Application.Recruitment.Models;
using NHCM.Application.Recruitment.Queries;
using NHCM.Domain.Entities;
using NHCM.WebUI.Types;

namespace NHCM.WebUI.Pages.Recruitment
{
    public class MilitaryServiceModel : BasePage
    {
        public async Task OnGet()
        {
            ListOfMilitaryServiceType = new List<SelectListItem>();

            List<MilitaryServiceType> service = new List<MilitaryServiceType>();
            service = await Mediator.Send(new GetMilitryServiceTypeQuery() { Id = null });
            foreach (MilitaryServiceType s in service)
                ListOfMilitaryServiceType.Add(new SelectListItem(s.Name, s.Id.ToString()));
        }



        public async Task<IActionResult> OnPostSave([FromBody] SavePersonMilitaryServiceCommand command)
        {
            try
            {

                command.CreatedBy = 10;
               
                command.CreatedOn = DateTime.Now;
                command.ModifiedOn = DateTime.Now;

                List<SearchedPersonMilitaryService> result = new List<SearchedPersonMilitaryService>();
                result = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {
                    Data = new { list = result },
                    Status = UIStatus.Success,
                    Text = " خدمت عسکری کارمند موفقانه ثبت سیستم شد",
                    Description = string.Empty

                });

            }
            catch (Exception ex)
            {
                return new JsonResult(new UIResult()
                {

                    Data = null,
                    Status = UIStatus.Failure,
                    Text = CustomMessages.StateExceptionTitle(ex),
                    Description = CustomMessages.DescribeException(ex)
                });
            }
        }

        public async Task<IActionResult> OnPostSearch([FromBody] SearchPersonMilitaryServiceQuery query)
        {
            try
            {
                List<SearchedPersonMilitaryService> dbResult = new List<SearchedPersonMilitaryService>();
                
                dbResult = await Mediator.Send(query);

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


        public async Task<IActionResult> OnPostDelete([FromBody] DeletePersonMilitaryServiceCommand command)
        {
            try
            {
                string dbResult = string.Empty;
                dbResult = await Mediator.Send(command);

                return new JsonResult(new UIResult()
                {

                    Data = new { list = dbResult },
                    Status = UIStatus.Success,
                    Text = " خدمت عسکری انتخاب شده موفقانه حذف شد",
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